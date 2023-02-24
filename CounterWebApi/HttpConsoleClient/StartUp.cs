using System.Net.Http.Json;
using System.Net.Http;
using HttpConsoleClient;

for (int i = 0; i < 100000; i++)
{
    await Task.Run(() => Calc());
}

static async Task Calc()
{

    HttpClient httpClient = new HttpClient();

    int customerId = DateTime.Now.Millisecond % 2 == 0 ? 2 : 1;
    var message = $"Thread {Thread.CurrentThread.ManagedThreadId} -> {customerId}";
    Console.WriteLine(message);

    using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
        "https://localhost:7277/api/Order", new CreateModelDto() { Name = message, Customer = customerId });

    var todo = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"{todo}\n");
}


