using MessagePack;
using Newtonsoft.Json;

var product = new Product()
{
    Name = "Apple",
    ExpireDate = new DateTime(2023, 01, 02),
    Sizes = new List<string> { "small", "medium", "large" },
    Price = 4.99m
};
var product1 = new Product()
{
    Name = "Android",
    ExpireDate = new DateTime(2023, 01, 02),
    Sizes = new List<string> { "small", "medium", "large" },
    Price = 2.99m
};

string output = JsonConvert.SerializeObject(product, Formatting.Indented);

var dP = JsonConvert.DeserializeObject<Product>(output);
Console.WriteLine(output);

var list = new List<Product>() { product, product1 };

byte[] bytes = MessagePackSerializer.Serialize(list);
Console.WriteLine(bytes.Length);

var deserializedList = MessagePackSerializer.Deserialize<List<Product>>(bytes);
foreach (var item in deserializedList)
{
    Console.WriteLine(item);
}

var json = MessagePackSerializer.SerializeToJson(list);

Console.WriteLine(json);

[MessagePackObject]
public record Product
{
    [Key(0)]
    public string Name { get; set; }

    [Key(1)]
    public DateTime ExpireDate { get; set; }

    [Key(2)]
    public decimal Price { get; set; }

    [Key(3)]
    public List<string> Sizes { get; set; }
}