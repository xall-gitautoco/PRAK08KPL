// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        Config Konf = new Config();

        Console.WriteLine("en/id");
        string inputLang = Console.ReadLine();

        if (inputLang == "en")
        {
            Console.WriteLine("Please insert the amout of money to trasnfer");
        } else if (inputLang == "id") {
            Console.WriteLine("Masukkan jumlah uang yang akan di-trasnfer");
        }

        int inputBiaya = Convert.ToInt32(Console.ReadLine());
        Konf.tranfer.threshold = 25000000;
        Konf.tranfer.low_fee = 6500;
        Konf.tranfer.high_fee = 15000;

        if (inputBiaya <= Konf.tranfer.threshold) {
            inputBiaya = inputBiaya + Konf.tranfer.low_fee;
        } else if (inputBiaya > Konf.tranfer.threshold)
        {
            inputBiaya = inputBiaya + Konf.tranfer.high_fee;
        }

        if (inputLang == "en")
        {
            Console.WriteLine("Trasnfer fee = " + Konf.tranfer.threshold + "and Total amount :" + inputBiaya);
        }
        else if (inputLang == "id")
        {
            Console.WriteLine("Biaya trasnfer =" + Konf.tranfer.threshold + "dan Total Biaya : " + inputBiaya);
        }
    }
       
        

    public class Config
    {
        public string lang { get; set; }
        public Transfer tranfer { get; set; }
        public List<string> methods { get; set; }
        public Confirm confirm { get; set; }

        public Config() { }

        public Config(string lang, Transfer tranfer, List<string> methods, Confirm confirm)
        {
            this.lang = lang;
            this.tranfer = tranfer;
            this.methods = methods;
            this.confirm = confirm;
        }
    }

    public class Transfer
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }

        public Transfer(int threeshold, int low_fee, int high_fee)
        {
            this.threshold = threeshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }

    public class Confirm
    {
        public string en { get; set; }
        public string id { get; set; }

        public Confirm(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }

    public class BankTrasnferConfig
    {
        public Config bankConfig;

        public const string filePath = "C:\\Programing\\C#\\Modul08_1302220093\\Modul08_1302220093\\bin\\Debug\\net8.0\\bank_transfer_config.json";

        public BankTrasnferConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        public void SetDefault()
        {
            Transfer tran = new Transfer(25000000, 6500, 15000);
            Confirm conf = new Confirm("yes", "ya");

            bankConfig = new Config("en", tran, ["RTO (real-time)", "SKN", "RTGS", "BI FAST"], conf);
        }

        public Config ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            bankConfig = JsonSerializer.Deserialize<Config>(configJsonData);
            return bankConfig;
        }

        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(bankConfig, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
