﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace AutomationPracticeProject.WrapperFactory
{
    public class WebDriverFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                
                return _driver;
            }

            private set => _driver = value;
        }

        public static void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    if (_driver == null)
                    {
                        _driver = new FirefoxDriver();
                        Drivers.Add("Firefox", Driver);
                    }
                    break;

                case "IE":
                    if (_driver == null)
                    {
                        _driver = new InternetExplorerDriver();
                        Drivers.Add("IE", Driver);
                    }
                    break;

                case "Chrome":
                    if (_driver == null)
                    {
                        _driver = new ChromeDriver(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../Drivers/"));
                        Drivers.Add("Chrome", Driver);
                        var options = new ChromeOptions();
                        options.AddArgument("no-sandbox");
                        _driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(70));
                    }
                    break;
            }
        }

        public static void GoToUrl(string url)
        {
            Driver.Url = url;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Quit();
            }

            Drivers.Clear();
            _driver = default;
        }
    }
}