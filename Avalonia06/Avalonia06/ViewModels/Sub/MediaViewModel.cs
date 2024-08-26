using Avalonia.Platform;
using Avalonia.Media.Imaging;

using DockModule06.WLIB.Models;
using ReactiveUI;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;


namespace Avalonia06.ViewModels.Sub;

public partial class MediaViewModel : ViewModelBase, IBaseDock
{
    public string Id { get; set; }

    public bool IsUsed { get; set; }
    public Avalonia.Media.Imaging.Bitmap? ImageSource { get; } = ImageHelper.LoadFromResource(new ("avares://Avalonia06/Assets/ASSET_MSI_MEG_GODLIKE.jpg"));

    private bool _GetTrue;
    public bool GetTrue
    {
        get => _GetTrue;
        protected set { this.RaiseAndSetIfChanged(ref _GetTrue, value); }
    }

    public ICommand TransformCommand { get; set; }
    public ICommand ReverseCommand { get; set; }
    
    public MediaViewModel()
    {
        var canTransform = this.WhenAnyValue(x => x.GetTrue);
        var canReverse = this.WhenAnyValue(x => x.GetTrue);
        
        // TransformCommand = ReactiveCommand.Create(OnTransform, canTransform);
        TransformCommand = ReactiveCommand.CreateFromTask(async () => await OnTransform(), canTransform);
        ReverseCommand = ReactiveCommand.CreateFromTask(OnReverse, canReverse);
    }

    [RelayCommand]
    public void Ready()
    {
        GetTrue = true;
    }

    private async Task OnTransform()
    {
        GetTrue = false;
        await Task.Run(async () =>
        {
            await using (MemoryStream stream = new MemoryStream())
            {
                ImageSource?.Save(stream); // Save the bitmap to a stream.
                stream.Seek(0, SeekOrigin.Begin); // Rewind the stream
                
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);
                
                var area = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var size = new Size(bitmap.Width, bitmap.Height);

                var bitmapData = bitmap.LockBits(area, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                for (int y = 0; y < bitmap.Height; ++y)
                {
                    int offset = bitmapData.Stride * y;

                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        byte B = Marshal.ReadByte(bitmapData.Scan0 + offset++);
                        byte G = Marshal.ReadByte(bitmapData.Scan0 + offset++);
                        byte R = Marshal.ReadByte(bitmapData.Scan0 + offset++);
                        byte A = Marshal.ReadByte(bitmapData.Scan0 + offset++);

                        int rgba = R | (G << 8) | (B << 16) | (A << 24);

                        // var ee = new MemoryStream(rgba);

                        // byte[] bb = ee.ToArray();
                        // IntPtr ptr = bitmapData.Scan0;
                        // int bytes = bitmapData.Stride * bitmap.Height;
                        // Marshal.Copy(bb, 0, ptr, bytes); // Copy Test
                    }
                }

                bitmap.UnlockBits(bitmapData);
                // bitmap.Save(file);
            }
        });
        GetTrue = true;
    }

    private async Task OnReverse()
    {
        GetTrue = false;
        await Task.Run(async () =>
        {
            await using (MemoryStream stream = new MemoryStream())
            {
                ImageSource?.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);
                var area = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                BitmapData bitmapData = bitmap.LockBits(area, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int byteCount = bitmapData.Stride * bitmapData.Height;
                byte[] pixels = new byte[byteCount];
                int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

                IntPtr pPixel = bitmapData.Scan0;

                Marshal.Copy(pPixel, pixels, 0, pixels.Length);

                int pixelHeight = bitmapData.Height;
                int pixelWidth = bitmapData.Width;

                int bitmapHeight = bitmap.Height;
                int bitmapWidth = bitmap.Width;

                int widthInBytes = bitmapData.Width * bytesPerPixel;

                for (int y = 0; y < pixelHeight; ++y)
                {
                    int current = y * bitmapData.Stride;
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel + 1)
                    {
                        byte blue = pixels[current + x];
                        byte green = pixels[current + x + 1];
                        byte red = pixels[current + x + 2];
                        // byte alpha = pixels[current + x + 3];

                        pixels[current + x] = (byte)(255 - blue);
                        pixels[current + x + 1] = (byte)(255 - green);
                        pixels[current + x + 2] = (byte)(255 - red);
                        // pixels[current + x + 3] = (byte)(255 - alpha);
                    }
                }

                Marshal.Copy(pixels, 0, pPixel, pixels.Length);
                bitmap.UnlockBits(bitmapData);

                bitmap.Save("output.jpg");
            }
        });
        GetTrue = true;
    }
}

public static class ImageHelper
{
    public static Avalonia.Media.Imaging.Bitmap LoadFromResource(Uri resourceUri)
    {
        Avalonia.Media.Imaging.Bitmap b = new Avalonia.Media.Imaging.Bitmap(AssetLoader.Open(resourceUri));
        return b;
    }

    public static async Task<Avalonia.Media.Imaging.Bitmap?> LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return new Avalonia.Media.Imaging.Bitmap(new MemoryStream(data));
            //return new System.Drawing.Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
            return null;
        }
    }
}
