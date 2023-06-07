// See https://aka.ms/new-console-template for more information
using TestQuestion2;

Console.Write("Time: ");
string time = Console.ReadLine();

Console.Write("Ano: ");
var ano = Console.ReadLine();

var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={ano}&team1={time}";

using (HttpClient client = new HttpClient())
{
    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();
    var partidas = Newtonsoft.Json.JsonConvert.DeserializeObject<PartidasRespostas>(responseBody);
    var gols = partidas.Data.Sum(x => decimal.Parse(x.Team1goals)).ToString();
    Console.WriteLine($"{time} - scored {gols} in {ano}");
}