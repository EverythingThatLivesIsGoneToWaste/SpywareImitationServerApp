using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using HttpMultipartParser;
using System.Diagnostics;

namespace StatusRemote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private HttpListener _server;
        private bool _isRunning;

        private async Task StartServerAsync()
        {
            _isRunning = true;
            _server = new HttpListener();
            _server.Prefixes.Add("http://26.122.151.249:8080/");

            try
            {
                _server.Start();
                AppendLog("Сервер запущен. Ожидание запросов...");

                while (_isRunning)
                {
                    try
                    {
                        var context = await _server.GetContextAsync();

                        Task.Run(() => ProcessRequest(context)).ConfigureAwait(false);
                    }
                    catch (HttpListenerException) when (!_isRunning)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        AppendLog($"Ошибка получения контекста: {ex.Message}");
                    }
                }
            }
            finally
            {
                try
                {
                    if (_server?.IsListening == true)
                    {
                        _server.Stop();
                    }
                    _server?.Close();
                }
                catch (ObjectDisposedException) {}
                catch (Exception ex)
                {
                    AppendLog($"Ошибка остановки сервера: {ex.Message}");
                }
            }
        }

        private async Task ProcessRequest(HttpListenerContext context)
        {
            try
            {
                if (context.Request.ContentType?.StartsWith("image/png") == true)
                {
                    await HandleImagePost(context);
                }
                else if (context.Request.ContentType?.StartsWith("multipart") == true)
                {
                    await HandleMultipleImages(context);
                }
                else if (context.Request.HttpMethod == "POST")
                {
                    await HandlePost(context);
                }
                else
                {
                    await HandleRequestAsync("Сервер ответил.", context);
                }

                AppendLog($"Обработан запрос от {context.Request.RemoteEndPoint}");
            }
            catch (Exception ex)
            {
                AppendLog($"Ошибка обработки запроса: {ex.Message}");
                try
                {
                    await SendErrorResponse(context, 500, "Internal Server Error");
                }
                catch {}
            }
            finally
            {
                context.Response.Close();
            }
        }

        private async Task HandleRequestAsync(string response, HttpListenerContext context)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }

        private async Task SendErrorResponse(HttpListenerContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }

        private async Task HandlePost(HttpListenerContext context)
        {
            try
            {
                using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    string receivedString = await reader.ReadToEndAsync();

                    Console.WriteLine("Получена строка.");
                    UpdateUI($"{receivedString}");

                    await HandleRequestAsync("Получена строка.", context);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"Ошибка обработки: {ex.Message}");
            }
        }

        private void UpdateUI(string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => UpdateUI(message)));
            }
            else
            {
                richTextBox1.Text += (message);
                richTextBox1.Text += "----------------" + Environment.NewLine;
            }
        }

        private async Task HandleImagePost(HttpListenerContext context)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    await context.Request.InputStream.CopyToAsync(ms);

                    ms.Position = 0;

                    if (ms.Length == 0)
                    {
                        await HandleRequestAsync("Изображение не найдено.", context);
                        return;
                    }

                    using (var msCopy = new MemoryStream(ms.ToArray()))
                    {
                        try
                        {
                            using (var image = Image.FromStream(msCopy))
                            {
                                string fileName = $"C:\\Users\\Desktop\\Desktop\\Screenshots\\Screen\\img_{DateTime.Now:yyyyMMddHHmmss}.png";

                                using (var saveStream = new MemoryStream())
                                {
                                    image.Save(saveStream, ImageFormat.Png);
                                    File.WriteAllBytes(fileName, saveStream.ToArray());
                                }
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Некорректные данные изображения: {ex.Message}");
                            return;
                        }
                    }
                    Console.WriteLine("Изображение получено.");
                    await HandleRequestAsync("Изображение получено.", context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private async Task HandleMultipleImages(HttpListenerContext context)
        {
            try
            {
                if (!context.Request.ContentType.Contains("multipart/form-data"))
                {
                    await HandleRequestAsync("Изображения не найдены.", context);
                    return;
                }

                var parser = await MultipartFormDataParser.ParseAsync(
                    context.Request.InputStream,
                    context.Request.ContentEncoding,
                    binaryBufferSize: 4096);

                var receivedFiles = new List<string>();

                int a = 0;
                foreach (var filePart in parser.Files)
                {
                    a++;
                    try
                    {
                        string fileName = $"C:\\Users\\Desktop\\Desktop\\Screenshots\\Windows\\img_{DateTime.Now:yyyyMMddHHmmss}_{a}.png";

                        using (var fileStream = File.Create(fileName))
                        {
                            await filePart.Data.CopyToAsync(fileStream);
                        }

                        receivedFiles.Add(fileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка обработки файла {filePart.Name}: {ex.Message}");
                    }
                }

                await HandleRequestAsync($"Получено {receivedFiles.Count} изображений.", context);
                Console.WriteLine($"Получено {receivedFiles.Count} изображений.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isRunning = false;
        }

        private void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AppendLog(message)));
                return;
            }

            textBoxLog.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
        }

        private void BtnToggleServer_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            if (btn.Text == "Запустить")
            {
                btn.Text = "Остановить";
                _ = StartServerAsync();
            }
            else
            {
                btn.Text = "Запустить";
                _isRunning = false;
                _server?.Stop();
            }
        }

        private void BtnOpenImageFolder_Click(object sender, EventArgs e)
        {
            string folderPath = $"C:\\Users\\Desktop\\Desktop\\Screenshots";


            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                MessageBox.Show("Папка не существует!");
            }
        }

        private void BtnOpenImageFolder_MouseEnter(object sender, EventArgs e)
        {

            panel2.BorderStyle = BorderStyle.Fixed3D;
        }

        private void BtnOpenImageFolder_MouseLeave(object sender, EventArgs e)
        {

            panel2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void BtnToggleServer_MouseEnter(object sender, EventArgs e)
        {
            panel1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void BtnToggleServer_MouseLeave(object sender, EventArgs e)
        {
            panel1.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
