using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Reflection.Metadata;

namespace Demo
{
    public class Program
    {
        static HttpClient client = new();
        private record PlayingField(string fieldName, int height, int width);

        private record User(int role, string firstName, string lastName);

        private record GetUser(string role, string firstName, string lastName);

        private record Ship(int speed, int health, int length, int shiptype);

        private record GetShip (int speed, int health, int length, string shipType);

        private record PutShip (int speed, int Health, int Length);

        static void Main(string[] args)
        {

            client.BaseAddress = new Uri("http://localhost:5236/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ShipRunAsync().GetAwaiter().GetResult();
            FieldRunAsnyc().GetAwaiter().GetResult();
            UserRunAsync().GetAwaiter().GetResult();
        }

        private static async Task ShipRunAsync()
        {
            Console.Clear();
            Console.WriteLine("Creating Ships....");
            Console.WriteLine();
            var beast = new Ship(2, 5, 3, 0);
            var predator = new Ship(2, 3, 3, 1);
            var shark = new Ship(2, 3, 3, 2);
            var beastJson = JsonSerializer.Serialize(beast);
            var predatorJson = JsonSerializer.Serialize(predator);
            var sharkJson = JsonSerializer.Serialize(shark);
            HttpContent content1 = new StringContent(sharkJson, Encoding.UTF8, "application/json");
            HttpContent content2 = new StringContent(predatorJson, Encoding.UTF8, "application/json");
            HttpContent content3 = new StringContent(beastJson, Encoding.UTF8, "application/json");
            string createPath = "ship";
            var uri1 = await CreateItemAsync(createPath, content1);
            var uri2 =await CreateItemAsync(createPath, content2);
            var uri3 =await CreateItemAsync(createPath, content3);
            Console.WriteLine("Created Ships at locations....");
            Console.WriteLine();
            await Console.Out.WriteLineAsync($"Created at {uri1}"); 
            await Console.Out.WriteLineAsync($"Created at {uri2}");
            await Console.Out.WriteLineAsync($"Created at {uri3}");
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Getting Ship");
            Console.WriteLine("Enter an Id ");
            int getId = Convert.ToInt32( Console.ReadLine());
            string getPath = $"ship/{getId}";
            var getShip = await GetItemAsync<GetShip>(getPath);
            Console.WriteLine($"speed: {getShip.speed}, health: {getShip.health}, length :{getShip.length}, shipType: {getShip.shipType}");
            Console.ReadKey();

            Console.WriteLine("Updating....");
            Console.WriteLine("Enter an Id ");
            int putId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Updates: speed :4, health, health, 4, length 4");
            Console.WriteLine();
            var updatedShip = new PutShip(4, 4, 4);
            var updatedJson = JsonSerializer.Serialize(updatedShip);
            var path = $"ship/{putId}";
            var url = await UpdateFieldAsync(updatedJson, putId, path);
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Getting all ships...");
            var getAllpath = "ship";
            List<GetShip> fields = await GetAllAsync<GetShip>(getAllpath);
            fields.ForEach(x =>
            {
                Console.WriteLine($"speed: {x.speed}, health: {x.health}, length :{x.length}, shipType: {x.shipType}");
            });
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Deleting ship.....");
            Console.WriteLine("Enter an id: ");
            int deleteId = Convert.ToInt32(Console.ReadLine());
            string deletePath = $"ship/{deleteId}";
            var statusCode = await DeleteItem(deletePath);
            await Console.Out.WriteLineAsync(statusCode.ToString());
            Console.ReadKey();


        }
        private static async Task UserRunAsync()
        {
            Console.Clear();
            Console.WriteLine("Creating users....");
            Console.WriteLine();
            var UserA = new User(0, "paul", "jake");
            var UserB = new User(1, "Martin", "Luther");
            var UserC = new User(2, "Lion", "Simba");
            var UserAJson = JsonSerializer.Serialize(UserA);
            var UserBJson = JsonSerializer.Serialize(UserB);
            var UserCJson = JsonSerializer.Serialize(UserC);
            HttpContent content1 = new StringContent(UserAJson, Encoding.UTF8, "application/json");
            HttpContent content2 = new StringContent(UserBJson, Encoding.UTF8, "application/json");
            HttpContent content3 = new StringContent(UserCJson, Encoding.UTF8, "application/json");
            var path = "user";
            var uriA = await CreateItemAsync(path, content1);
            var uriB = await CreateItemAsync(path, content2);
            var uriC = await CreateItemAsync(path, content3);
            await Console.Out.WriteLineAsync("Users Created at location....");
            Console.WriteLine();
            await Console.Out.WriteLineAsync($"Created at {uriA}");
            await Console.Out.WriteLineAsync($"Created at {uriB}");
            await Console.Out.WriteLineAsync($"Created at {uriC}");
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Getting User by Id");
            Console.WriteLine("Enter an Id ");
            int getId = Convert.ToInt32(Console.ReadLine());
            string getPath = $"user/{getId}";
            var getUser = await GetItemAsync<GetUser>(getPath);
            Console.WriteLine($"FirstName : {getUser.firstName}, Lastname: {getUser.lastName}, Role : {getUser.role}");
            Console.ReadKey();
        }

        private static async Task FieldRunAsnyc()
        {
            Console.Clear();
            Console.WriteLine("Creating Playingfields.....");
            Console.WriteLine();
            var Etihad = new PlayingField("Etihad", 7, 7);
            var Emirates = new PlayingField("emirates", 9, 9);
            var etihadJson = JsonSerializer.Serialize(Etihad);
            var emiratesJson = JsonSerializer.Serialize(Emirates);
            var path = "playingfield";
            HttpContent content1 = new StringContent(etihadJson, Encoding.UTF8, "application/json");
            HttpContent content2 = new StringContent(emiratesJson, Encoding.UTF8, "application/json");
            var uri1 = await CreateItemAsync(path, content1);
            var uri2 = await CreateItemAsync(path, content2);
            Console.WriteLine("PlayingField Created at locations: ");
            await Console.Out.WriteLineAsync($"Created at {uri1}");
            Console.WriteLine();
            await Console.Out.WriteLineAsync($"Created at {uri2}");
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Showing Validation");
            Console.WriteLine("FieldName = string.empty, Height = 4, width = 4");
            Console.WriteLine();
            var validationError = new PlayingField("", 4, 4);
            var errorJson = JsonSerializer.Serialize(validationError);
            HttpContent content3 = new StringContent(errorJson, Encoding.UTF8, "application/json");
            var uri3 = await CreateItemAsync(path, content3);
            await Console.Out.WriteLineAsync($"Created at {uri3}");
            Console.ReadKey();

            Console.WriteLine("Getting playing Field");
            Console.WriteLine("Enter an id: ");
            int id = Convert.ToInt32( Console.ReadLine() );
            string getPath = $"playingfield/{id}";
            var getField = await GetItemAsync<PlayingField>(getPath);
            Console.WriteLine($"name: {getField.fieldName}, height: {getField.height}, width :{getField.width}");
            Console.ReadKey();

            Console.WriteLine("Getting all PlayingFields...");
            var getAllpath = "playingfield";
            List<PlayingField> fields = await GetAllAsync<PlayingField>(getAllpath);
            fields.ForEach(x =>
            {
                Console.WriteLine($"name: {x.fieldName}, height: {x.height}, width :{x.width}");
            });
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine("Deleting playingFields.....");
            Console.WriteLine("Enter an id: ");
            int deleteId = Convert.ToInt32(Console.ReadLine());
            string deletePath = $"playingField/{deleteId}";
            var statusCode = await DeleteItem(deletePath);
            await Console.Out.WriteLineAsync(statusCode.ToString());
            Console.ReadKey();

        }
       
        private static async Task UserSetup()
        {
            Console.WriteLine("User Login...");
            Console.WriteLine("Enter Id of user..");
            int id = Convert.ToInt32(Console.ReadLine());
            var url = $"usersetup/{id}";
        }
        private static async Task<Uri?> CreateItemAsync(string path, HttpContent content)
        {
            HttpResponseMessage response = await client.PostAsync(path, content);
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            return response.Headers.Location;
        }

        private static async Task<T> GetItemAsync<T>(string path)
        {
            T? item = default(T?);
            HttpResponseMessage response = await client.GetAsync(path);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

            item = await response.Content.ReadFromJsonAsync<T>();
            return item;
        }

        private static async Task<HttpStatusCode> UpdateFieldAsync(string json, int id, string path)
        {
            HttpContent content = new StringContent(json,Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(path, content);
            response.EnsureSuccessStatusCode();
            return response.StatusCode;
        }

        private static async Task<List<T?>> GetAllAsync<T>(string path)
        {
            List<T> list = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<T>>();
            }
            return list;
        }

        private static async Task<HttpStatusCode> DeleteItem(string path)
        {
            HttpResponseMessage response = await client.DeleteAsync(path);
            if (!response.IsSuccessStatusCode )
            {
                var message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

            return response.StatusCode;
        }

        //private static async Task<>
    }
}
