Библеотека использует .NET Core, так же перед использованием важно знать что класс **SpClient** является ***асинхронным*** (то есть к каждому методу этого класса нужно применять ключевое слово await)

Перед тем как кидать свои Issues по-типу: "Почему мои события не работают" убедитесь что вы используете только ***один*** экземпляр класса **SpClient**, такого результата можно достичь применив паттерн Singleton или механизм Dependency Injection (DI).

Разработчик данной библиотеки рекомендует использовать только одно событие (event) на один IP-address. Использование всех трёх событий одновременно может привести к неожиданному исключению в вашем приложении вызванном из-за RateLimit'a которые задали разработчики API.

Примеры: 

1. Получение всех игроков сервера:
```csharp
var client = new SpClient();
var players = await client.GetOnlinePlayersAsync();
Console.WriteLine(String.Join("\n", players.ServerPlayers.Select(x => x.Nickname)));
Console.ReadKey();
```

2. Получение записей из чата: 
```csharp
var client = new SpClient();
var messages = await client.GetChatMessagesAsync();
foreach(var message in messages)
{
Console.WriteLine($"{message.Time}:{message.Author} -- {message.Content}");
}
Console.ReadKey();
```
3. Получение времени суток: 
```csharp 
var client = new SpClient();
var daytime = await client.GetDayTimeAsync();
Console.WriteLine($"{daytime.DayTime} -- {daytime.Ticks}");
Console.ReadKey();
```

4. Получение погоды: 
```csharp 
var client = new SpClient();
var weather = await client.GetWeatherAsync();
Console.WriteLine(weather);
Console.ReadKey();
```

5. Подписка на ивент ***MessageAdd***:
```csharp
var client = new SpClient();
client.MessageAdd += async (sender, e) => {Console.WriteLine($"{e.Author} - {e.Content}"); };
await Task.Delay(-1);
```
Подписки на остальные события соершаются аналогично. 
