Библеотека использует .NET Core, так же перед использованием стоит понять что класс **SpClient** является ***асинхронным*** (то есть к каждому методу этого класса нужно применять ключевое слово await)

Примеры: 

1. Получение всех игроков сервера
```csharp
var client = new SpClient();
var players = await client.GetOnlinePlayersAsync();
Console.WriteLine(String.Join("\n", players.ServerPlayers.Select(x => x.Nickname)));
Console.ReadKey();
```
