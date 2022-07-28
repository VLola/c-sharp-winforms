using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

const int PORT = 8088;
const string IP = "127.0.0.1";
Console.WriteLine("Server start...");
IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
try
{
    Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    serverSocket.Bind(iPEnd);
    serverSocket.Listen(10);
    while (true)
    {
        Socket clientSocket = serverSocket.Accept();
        int bytes = 0;
        byte[] buffer = new byte[1024];
        StringBuilder builder = new StringBuilder();
        do
        {
            bytes = clientSocket.Receive(buffer);
            builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
        } while (clientSocket.Available > 0);
        byte[] data = Encoding.Unicode.GetBytes(Calculate(builder.ToString()));
        Console.WriteLine("jndtn");
        clientSocket.Send(data);
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
    serverSocket.Shutdown(SocketShutdown.Both);
    serverSocket.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


Console.WriteLine("Server end...");

Console.ReadLine();

string Calculate(string message)
{
    (string x, string y, string symbol) variable = JsonConvert.DeserializeObject<(string x, string y, string symbol)>(message);
    string answer = "";
    double? x = Double.Parse(variable.x);
    double? y = Double.Parse(variable.y);
    if (x != null && y != null)
        switch (variable.symbol)
        {
            case "+":
                answer = ((double)x + (double)y).ToString();
                break;
            case "-":
                answer = ((double)x - (double)y).ToString();
                break;
            case "/":
                answer = ((double)x / (double)y).ToString();
                break;
            case "*":
                answer = ((double)x * (double)y).ToString();
                break;
            default:
                break;
        }
    return "" + answer;
}
