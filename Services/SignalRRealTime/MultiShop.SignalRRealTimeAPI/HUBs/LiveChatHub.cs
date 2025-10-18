using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

public class LiveChatHub : Hub
{
    // 🔹 Aktif kullanıcı oturumlarını (SessionID, ConnectionID) olarak tutar
    private static ConcurrentDictionary<string, string> ActiveSessions = new();

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var role = httpContext.Request.Query["role"].ToString();
        var username = httpContext.Request.Query["username"].ToString() ?? "Kullanıcı";

        if (role == "admin")
        {
            // 🔸 Admin gruba eklenir
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            await Clients.Caller.SendAsync("ReceiveMessage", "Sistem", "✅ Admin bağlandı", "system");
        }
        else
        {
            // 🔹 Her kullanıcı için benzersiz SessionID üret
            string sessionId = Guid.NewGuid().ToString("N");

            // 🔹 SessionID - ConnectionID eşleşmesini kaydet
            ActiveSessions[sessionId] = Context.ConnectionId;

            // 🔹 Kullanıcıyı kendi grubuna ekle
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);

            // 🔹 Admin’e bilgi gönder
            await Clients.Group("Admins").SendAsync("ReceiveMessage", username, $"Yeni kullanıcı bağlandı. SessionID: {sessionId}", "system", sessionId);

            // 🔹 Admin tarafında sekme oluşturulmasını tetikler
            await Clients.Group("Admins").SendAsync("NewUserConnected", username, sessionId);

            // 🔹 Kullanıcıya kendi SessionID’sini gönder
            await Clients.Caller.SendAsync("SetSessionId", sessionId);
        }

        await base.OnConnectedAsync();
    }

    // 🔹 Kullanıcı veya admin mesaj gönderdiğinde
    public async Task SendMessage(string user, string message, string role, string sessionId = "")
    {
        if (role == "admin" && !string.IsNullOrEmpty(sessionId))
        {
            // 🔸 Admin kullanıcıya mesaj gönderiyor
            if (ActiveSessions.ContainsKey(sessionId))
            {
                await Clients.Group(sessionId).SendAsync("ReceiveMessage", "Admin", message, "admin");
                await Clients.Group("Admins").SendAsync("ReceiveMessage", "Admin", $"({sessionId}) kullanıcısına mesaj gönderildi: {message}", "system");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Sistem", $"❌ Session bulunamadı: {sessionId}", "system");
            }
        }
        else if (role == "user" && !string.IsNullOrEmpty(sessionId))
        {
            // 🔸 Kullanıcıdan gelen mesaj admin grubuna gönderilir
            await Clients.Group("Admins").SendAsync("ReceiveMessage", user, message, role, sessionId);
        }
    }

    // 🔹 Admin doğrudan belirli kullanıcıya mesaj göndermek isterse (UI'dan sessionId ile)
    public async Task SendMessageToSession(string adminName, string message, string role, string sessionId)
    {
        if (ActiveSessions.ContainsKey(sessionId))
        {
            string connectionId = ActiveSessions[sessionId];
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", adminName, message, role);
            await Clients.Group("Admins").SendAsync("ReceiveMessage", "Admin", $"({sessionId}) kullanıcısına mesaj gönderildi: {message}", "system");
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Sistem", $"❌ Oturum bulunamadı: {sessionId}", "system");
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // 🔹 Kullanıcı ayrıldığında dictionary’den çıkar
        var kvp = ActiveSessions.FirstOrDefault(x => x.Value == Context.ConnectionId);
        if (!string.IsNullOrEmpty(kvp.Key))
        {
            ActiveSessions.TryRemove(kvp.Key, out _);

            // 🔹 Admin’e bilgi gönder
            await Clients.Group("Admins").SendAsync("ReceiveMessage", "Sistem", $"Kullanıcı oturumu kapattı ({kvp.Key})", "system");

            // 🔹 Sekme kaldırma işlemini tetikler
            await Clients.Group("Admins").SendAsync("UserDisconnected", kvp.Key);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
