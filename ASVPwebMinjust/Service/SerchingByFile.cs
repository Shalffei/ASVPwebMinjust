using System.Text;
using RestSharp;
using System.Text.Json;
using ASVPwebMinjust.Models.ModelsForPostRequest;
using ASVPwebMinjust.Models.ModelsForResponse;
using ASVPwebMinjust.Models.ModelsForStreamReader;
using DocumentFormat.OpenXml.Office2013.Word;
using Person = ASVPwebMinjust.Models.ModelsForStreamReader.Person;

namespace ASVPwebMinjust.Service
{
    public class SerchingByFile
    {
        public List<Result> ReadFileWithPeople (string path)
        {
            List<Person> people = new List<Person>();
            using (StreamReader reader = new StreamReader(path, Encoding.Default, true))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var partsName = line.Split(' ').ToList();
                    if (partsName.Count > 3)
                    {
                        string spase = " ";
                        partsName[0] = partsName[0] += spase += partsName[1];
                        partsName.RemoveAt(1);
                    }
                    if (partsName.Count < 3)
                    {
                        var partsWithDate = partsName[1].Split(',');
                        if (people.Select(x => x.Ipn5First).Contains(Convert.ToInt32(partsWithDate[1])))
                            continue;
                        DateTime dateTime = new DateTime(1900, 1, 1);
                        dateTime = dateTime.AddDays(Convert.ToDouble(Convert.ToInt32(partsWithDate[1]) - 1));
                        Person personWithoutPatronymic = new Person() { FirstName = partsName[1], LastName = partsName[0], MiddleName = null, dateOfBirth = dateTime, Ipn5First = Convert.ToInt32(partsWithDate[1]) };
                        people.Add(personWithoutPatronymic);
                    }
                    else
                    {
                        var partsWithDate = partsName[2].Split(',');
                        if (people.Select(x => x.Ipn5First).Contains(Convert.ToInt32(partsWithDate[1])))
                            continue;
                        DateTime dateTime = new DateTime(1900, 1, 1);
                        dateTime = dateTime.AddDays(Convert.ToDouble(Convert.ToInt32(partsWithDate[1]) - 1));
                        Person person = new Person() { FirstName = partsName[1], LastName = partsName[0], MiddleName = partsWithDate[0], dateOfBirth = dateTime, Ipn5First = Convert.ToInt32(partsWithDate[1]) };
                        people.Add(person);
                    }
                }
            }
            return PostRequestByPerson(people);
        }
        private List<Result> PostRequestByPerson(List<Person> people)
        {
            var client = new RestClient("https://asvpweb.minjust.gov.ua");
            List<Result> results = new List<Result>();
            foreach (var person in people)
            {
                DebtFilter debtfilter = new DebtFilter() { LastName = person.LastName, FirstName = person.FirstName, MiddleName = person.MiddleName, BirthDate = person.dateOfBirth};
                CreditFilter creditFilter = new CreditFilter() { LastName = "", FirstName = "", MiddleName = ""};
                Filter filter = new Filter() { debtFilter = debtfilter, creditFilter = creditFilter, VPNum = "", vpOpenFrom = "", vpOpenTo = ""};
                Root rootForPostPerson = new Root() { filter = filter, reCaptchaAction = "", reCaptchaToken = "", searchType = "11" };
                var request = new RestRequest("/listDebtCredVPEndpoint", Method.Post);
                request.AddJsonBody(JsonSerializer.Serialize(rootForPostPerson));
                var response = client.Execute(request);
                var deserialize = JsonSerializer.Deserialize<RootResponse>(response.Content);
                foreach(var result in deserialize.results)
                {
                    results.Add(result);
                }
                Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);
                if (response.IsSuccessful != true)
                {
                    while (response.IsSuccessful == false)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(30).Milliseconds);
                        response = client.Execute(request);
                    }
                }
            }
            foreach (var person in people)
            {
                DebtFilter debtfilter = new DebtFilter() { LastName = "", FirstName = "", MiddleName = "", BirthDate = null };
                CreditFilter creditFilter = new CreditFilter() { LastName = person.LastName, FirstName = person.FirstName, MiddleName = person.MiddleName };
                Filter filter = new Filter() { debtFilter = debtfilter, creditFilter = creditFilter, VPNum = "", vpOpenFrom = "", vpOpenTo = "" };
                Root rootForPostPerson = new Root() { filter = filter, reCaptchaAction = "", reCaptchaToken = "", searchType = "11" };
                var request = new RestRequest("/listDebtCredVPEndpoint", Method.Post);
                request.AddJsonBody(JsonSerializer.Serialize(rootForPostPerson));
                var response = client.Execute(request);
                var deserialize = JsonSerializer.Deserialize<RootResponse>(response.Content);
                foreach (var result in deserialize.results)
                {
                    results.Add(result);
                }
                Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);
                if (response.IsSuccessful != true)
                {
                    while (response.IsSuccessful == false)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(30).Milliseconds);
                        response = client.Execute(request);
                    }
                }
            }
            return results.GroupBy(x => x.orderNum).Select(x => x.FirstOrDefault()).ToList();
        }
        public List<Result> ReadFileWithCode (string path)
        {
            var codeList = new List<string>();
            using (StreamReader reader = new StreamReader(path, Encoding.Default, true))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    codeList.Add(line);
                }
            }
            
            return PostRequestByCode(codeList);
        }
        public List<Result> PostRequestByCode (List<string> codes)
        {
            var client = new RestClient("https://asvpweb.minjust.gov.ua");
            List<Result> results = new List<Result>();
            foreach (string code in codes)
            {
                DebtFilter debtfilter = new DebtFilter() { LastName = "", FirstName = "", MiddleName = "", BirthDate = null };
                CreditFilter creditFilter = new CreditFilter() { LastName = "", FirstName = "", MiddleName = "" };
                Filter filter = new Filter() { debtFilter = debtfilter, creditFilter = creditFilter, VPNum = code, vpOpenFrom = "", vpOpenTo = "" };
                Root rootForPostPerson = new Root() { filter = filter, reCaptchaAction = "", reCaptchaToken = "", searchType = "11" };
                var request = new RestRequest("/listDebtCredVPEndpoint", Method.Post);
                request.AddJsonBody(JsonSerializer.Serialize(rootForPostPerson));
                var response = client.Execute(request);
                var deserialize = JsonSerializer.Deserialize<RootResponse>(response.Content);
                foreach (var result in deserialize.results)
                {
                    results.Add(result);
                }
                Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);
                if (response.IsSuccessful != true)
                {
                    while (response.IsSuccessful == false)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(30).Milliseconds);
                        response = client.Execute(request);
                    }
                }
            }
            return results.GroupBy(x => x.orderNum).Select(x => x.FirstOrDefault()).ToList();
        }
    }
}
