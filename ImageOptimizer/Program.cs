using SixLabors.ImageSharp.Formats.Webp;

Console.WriteLine("ImageOptimizer by Kamil Prorok (https://github.com/kimpiks/)");
Console.WriteLine("Put all images in the same folder as this program");
Console.WriteLine("Settings:");
Console.Write("> Quality (0-100): ");

var quality = int.TryParse(Console.ReadLine(), out var q) ? q : 100;
switch (quality)
{
    case > 100:
        quality = 100;
        break;
    case < 0:
        quality = 0;
        break;
}

var path = Directory.GetCurrentDirectory();
var outputDir = Path.Combine(path, "output");
var files = Directory.GetFiles(path);

if (!Directory.Exists(outputDir))
{
    Directory.CreateDirectory(outputDir);
}

foreach (var file in files)
{
    if (!file.EndsWith(".jpg") &&
        !file.EndsWith(".jpeg") &&
        !file.EndsWith(".png"))
    {
        continue;
    }

    var extension = Path.GetExtension(file);

    using var image = Image.Load(file);
    var fileName = Path.GetFileName(file);
        
    await image.SaveAsWebpAsync(Path.Combine(outputDir, fileName).Replace(extension, ".webp"), new WebpEncoder()
    {
        Quality = quality
    });
}