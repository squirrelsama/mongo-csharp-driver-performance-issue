using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace MongoDriverPerf
{
    class Program
	{
        public static Random Rand = new Random();
        public static MongoCollection<BsonDocument> Acorns;

		public static void Main(string[] args)
		{
            var connectionUrl = new MongoUrl("mongodb://localhost");
            var mongoClientSettings = MongoClientSettings.FromUrl(connectionUrl);
            mongoClientSettings.WriteConcern = WriteConcern.Acknowledged;
            var client = new MongoClient(mongoClientSettings);
            var server = client.GetServer();
            Acorns = server.GetDatabase("FOOds").GetCollection("acorns");

            var threads = args.Count() > 0 
                ? int.Parse(args[0]) 
                : 100;

            var times = args.Count() > 1 
                ? int.Parse(args[1]) 
                : 10000;

            MongoStat.Watch(Console.Error, () =>
            {
                var w = System.Diagnostics.Stopwatch.StartNew();
                var alldone = Task.Factory.ContinueWhenAll(
                        Enumerable.Range(0, threads)
                        .Select(i => Task.Factory.StartNew(() => MakeRequests(i, times)))
                        .ToArray(), tasks => tasks)
                    .Result;
                w.Stop(); 
                Console.WriteLine("\t--------------------------");
                Console.WriteLine("\t{0} took {1}ms", Assembly.GetAssembly(typeof(MongoDatabase)).GetName().Version,  w.ElapsedMilliseconds); 
                Console.WriteLine("\t--------------------------");

            });
        }
        public static void MakeRequests(int id, int times)
        {
            Console.Error.WriteLine("{0} connected", id);
            foreach (var time in Enumerable.Range(0, times))
            {
                Acorns.Insert(new Acorn());
            }
            foreach (var time in Enumerable.Range(0, times))
            {
                Acorns.FindOneById(Rand.Next(0, id * times));
            }
            Console.Error.WriteLine("{0} done", id);
        }
	}
}
