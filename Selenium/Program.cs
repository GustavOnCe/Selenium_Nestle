using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Windows;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Selenium
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
           //Se crea la lista viajes
            var viajes = new List<string>();
            //Se asignan valores a la lista a travez de consola   
            Console.Title = "NESTLE CARTA PORTE";
            Console.WriteLine("Ingrese el/los numeros de viaje");
            var i = 0;
            while (i < 100) {
                string numero_buscar = Console.ReadLine();

                if (numero_buscar != "")
                {
                    viajes.Add(numero_buscar);
                    i++;

                }
                else {
                    break;
                }
            }
            
            //Se abre el navegador en la pagina rcontrol
            ChromeDriver controlador = new ChromeDriver();
            controlador.Navigate().GoToUrl("https://app.rcontrol.com.mx/corp/index.html");
            controlador.Manage().Window.Maximize();

            //Se logea en el portal y va al apartado NESTLE_CARTA
            Thread.Sleep(2000);
            IWebElement usuario = controlador.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[1]/input"));
            usuario.SendKeys("tms_ccpmigue");
            IWebElement contraseña = controlador.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[2]/input"));
            contraseña.SendKeys("Monroy2021$");
            IWebElement clickEntrar = controlador.FindElement(By.XPath("/html/body/div[1]/div/div/form/div[3]/input"));
            clickEntrar.Click();
            Thread.Sleep(2000);
            IWebElement clickCP = controlador.FindElement(By.XPath("/html/body/div[2]/header/div[2]/div/div[1]/div[1]/ul/li[3]/a/i"));
            clickCP.Click();
            IWebElement clickCargar = controlador.FindElement(By.XPath("/html/body/div[2]/header/div[2]/div/div[1]/div[1]/ul/li[3]/ul/li/a"));
            clickCargar.Click();
            Thread.Sleep(6000);
            IWebElement clickNestle = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[3]/div[2]/div/div/ul/li[16]"));
            clickNestle.Click();

            //Descarga los distintos viajes ingresados desde la consola

            for (int x = 0; x < viajes.Count; x++)
            {
                IWebElement clickBuscar = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[5]/button"));
                clickBuscar.Click();
                Thread.Sleep(3000);
                IWebElement viaje = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[5]/div/div[2]/div/label/input"));
                viaje.SendKeys(viajes[0 + x]);
                Thread.Sleep(1500);
                IWebElement limpiar_viaje = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[5]/div/div[2]/div/label/input"));
                limpiar_viaje.Clear();
                Thread.Sleep(1500);
                IWebElement clickBuscarC = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[5]/button"));
                clickBuscarC.Click();
                Thread.Sleep(2000);
                try
                {
                    IWebElement clickDetalle = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[4]/table/tbody/tr[1]/td[6]/button"));
                    clickDetalle.Click();
                    Thread.Sleep(1500);
                    IWebElement clickDescarga = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[2]/div[2]/div/div/ul/li/a"));
                    clickDescarga.Click();
                    Thread.Sleep(1500);
                    IWebElement contraer = controlador.FindElement(By.XPath("/html/body/div[2]/div/div/div/div/section/div[2]/span[1]"));
                    contraer.Click();
 
                }
                //Si el viaje no existe manda un mensaje a la consola y continua con los demas viajes
                catch
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n Viaje {0} no encontrado. Favor de validarlo",viajes[x]);
                    //Si la lista no contiene mas viajes por descargar, finaliza el proceso
                    if (viajes.Count == 1)
                    {
                        controlador.Close();
                        Console.WriteLine(":::::::::::::EL PROCESO FINALIZO:::::::::::::");
                        break;
                    }
                }

               


                Thread.Sleep(1500);
            }
            //Declaramos las rutas de donde se van a copiar y pegar los csv´s
            string sourcePath = @"C:\Users\ThinkPad\Downloads\";
            string targetPath = @"C:\Users\ThinkPad\Desktop\ADJUNTOS\";

            //Creamos un arreglo para almacenar los archivos descargados
            string[] files = Directory.GetFiles(sourcePath, "*.csv");

            //Llenamos el arreglo con los archivos y los copiamos a la carpeta especificada 
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    try {
                        string filename = Path.GetFileName(file);
                        string destino = Path.Combine(targetPath, filename);

                        File.Copy(file, destino);

                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    controlador.Close();
                    Thread.Sleep(10000);
                    Console.WriteLine(":::::::::::::EL PROCESO FINALIZO:::::::::::::");

                }
            }
            else 
            {
                Console.WriteLine(":::::::::::::ARCHIVO NO EXISTE:::::::::::::");
                
            }

           


            

        }
    }
}
