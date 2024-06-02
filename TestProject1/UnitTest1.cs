using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject1
{
    public class Test : IDisposable
    {
        private readonly ChromeDriver driver;

        public Test() 
        { 
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("file:///C:/Users/Brigita/Documents/__Personal__/Test-Projekt/index.html");
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Test]
        public void DobottSzam1es6Kozott()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            elsoGomb.Click();

            var dobottSzam = int.Parse(driver.FindElement(By.Id("dobottszam")).Text);
            Assert.That(dobottSzam, Is.InRange(1, 6), "A dobott számnak 1 és 6 között kell lennie.");
        }

        [Test]
        public void ElsoKarakterMozgasa()
        {
            var kezdetiPozicio = driver.FindElements(By.CssSelector(".elsmezo.p1")).Count == 1 ? driver.FindElements(By.CssSelector(".elsmezo.p1"))[0] : null;

            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            elsoGomb.Click();

            var vegsoPozicio = driver.FindElements(By.CssSelector(".elsmezo.p1")).Count == 1 ? driver.FindElements(By.CssSelector(".elsmezo.p1"))[0] : null;

            Assert.NotNull(kezdetiPozicio, "A kezdeti pozíció nem nulla.");
            Assert.NotNull(vegsoPozicio, "A vegső pozíció nem nulla.");
            Assert.That(vegsoPozicio, Is.Not.EqualTo(kezdetiPozicio), "Az első karakternek pozíciót kellett váltania.");
        }

        [Test]
        public void MasodikKarakterMozgasa()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            elsoGomb.Click();

            var kezdetiPozicio = driver.FindElements(By.CssSelector(".masodmezo.p2")).Count == 1 ? driver.FindElements(By.CssSelector(".masodmezo.p2"))[0] : null;

            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            masodikGomb.Click();

            var vegsoPozicio = driver.FindElements(By.CssSelector(".masodmezo.p2")).Count == 1 ? driver.FindElements(By.CssSelector(".masodmezo.p2"))[0] : null;

            Assert.NotNull(kezdetiPozicio, "A kezdeti pozíció nem nulla.");
            Assert.NotNull(vegsoPozicio, "A vegső pozíció nem nulla.");
            Assert.That(vegsoPozicio, Is.Not.EqualTo(kezdetiPozicio), "A második karakternek pozíciót kellett váltania.");
        }

        [Test]
        public void GombokAktivitasa()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));

            elsoGomb.Click();
            Assert.IsFalse(elsoGomb.Enabled, "Az első gomb működése megnyomása után inaktív kellene, hogy legyen.");
            Assert.IsTrue(masodikGomb.Enabled, "A második gomb működése az első megnyomása után aktívnak kellene, hogy legyen.");

            masodikGomb.Click();
            Assert.IsFalse(masodikGomb.Enabled, "A második gomb működése megnyomása után inaktív kellene, hogy legyen.");
            Assert.IsTrue(elsoGomb.Enabled, "Az első gomb működése a második megnyomása után aktívnak kellene, hogy legyen.");
        }

        [Test]
        public void ElsoKarakterHelyesSzamuMezotUgrik()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            var kezdetiPozicio = 0;

            for (int i = 0; i < 5; i++)
            {
                driver.ExecuteScript($"document.getElementById('dobottszam').innerHTML = '{i + 1}';");
                elsoGomb.Click();
                var ujPozicio = driver.FindElements(By.CssSelector(".elsmezo.p1")).Count == 1 ? Array.IndexOf(driver.FindElements(By.CssSelector(".elsmezo")).ToArray(), driver.FindElement(By.CssSelector(".elsmezo.p1"))) : -1;
                Assert.That(ujPozicio, Is.EqualTo(kezdetiPozicio + (i + 1)), $"A karakternek {i + 1} mezot kellene ugrania.");
                kezdetiPozicio = ujPozicio;
                driver.ExecuteScript($"document.getElementById('dobottszam').innerHTML = '{1}';");
                masodikGomb.Click();
            }
        }

        [Test]
        public void MasodikKarakterHelyesSzamuMezotUgrik()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            var kezdetiPozicio = 0;

            for (int i = 0; i < 5; i++)
            {
                driver.ExecuteScript($"document.getElementById('dobottszam').innerHTML = '{1}';");
                elsoGomb.Click();
                driver.ExecuteScript($"document.getElementById('dobottszam').innerHTML = '{i + 1}';");
                masodikGomb.Click();
                var ujPozicio = driver.FindElements(By.CssSelector(".elsmezo.p2")).Count == 1 ? Array.IndexOf(driver.FindElements(By.CssSelector(".elsmezo")).ToArray(), driver.FindElement(By.CssSelector(".elsmezo.p2"))) : -1;
                Assert.That(ujPozicio, Is.EqualTo(kezdetiPozicio + (i + 1)), $"A karakternek {i + 1} mezot kellene ugrania.");
                kezdetiPozicio = ujPozicio;
                driver.ExecuteScript($"document.getElementById('dobottszam').innerHTML = '{1}';");
                elsoGomb.Click();
            }
        }

        [Test]
        public void ElsoKarakterTullepesEsetenElsoMezore()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '20';");
            elsoGomb.Click();

            var ujPozicio = driver.FindElements(By.CssSelector(".elsmezo.p1")).Count == 1 ? Array.IndexOf(driver.FindElements(By.CssSelector(".elsmezo")).ToArray(), driver.FindElement(By.CssSelector(".elsmezo.p1"))) : -1;
            Assert.That(ujPozicio, Is.EqualTo(0), "Az első karakternek vissza kellet ugrania az első mezőre.");
        }

        [Test]
        public void MasodikKarakterTullepesEsetenElsoMezore()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            elsoGomb.Click();

            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '20';");
            masodikGomb.Click();

            var ujPozicio = driver.FindElements(By.CssSelector(".elsmezo.p2")).Count == 1 ? Array.IndexOf(driver.FindElements(By.CssSelector(".elsmezo")).ToArray(), driver.FindElement(By.CssSelector(".elsmezo.p2"))) : -1;
            Assert.That(ujPozicio, Is.EqualTo(0), "A második karakternek vissza kellet ugrania az első mezőre.");
        }

        [Test]
        public void UtolsoMezoEleresekorAGombokElerhetetlenek()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '19';");
            elsoGomb.Click();

            Assert.IsFalse(elsoGomb.Displayed, "Az első gomb elérhetetlen kell hogy legyen.");
            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            Assert.IsFalse(masodikGomb.Displayed, "A második gomb elérhetetlen kell hogy legyen.");
        }

        [Test]
        public void ElsoKarakterUtolsoMezoEleresekorAHatterNyertesnekMegfeleloSzinu()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '19';");
            elsoGomb.Click();

            var body = driver.FindElement(By.TagName("body"));
            Assert.That(body.GetCssValue("background-color"), Is.EqualTo("lime"), "Az első karakter győzelmekor a háttérnek lime színűvé kell változnia.");
        }

        [Test]
        public void MasodikKarakterUtolsoMezoEleresekorAHatterNyertesnekMegfeleloSzinu()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            elsoGomb.Click();

            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '19';");
            masodikGomb.Click();

            var body = driver.FindElement(By.TagName("body"));
            Assert.That(body.GetCssValue("background-color"), Is.EqualTo("darkred"), "A második karakter győzelmekor a háttérnek sötét piros színűvé kell változnia.");
        }

        [Test]
        public void ElsoKarakterGyozelmekorDobottSzamAKarakterNevereValtozik()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '19';");
            elsoGomb.Click();

            var dobottSzam = driver.FindElement(By.Id("dobottszam")).Text;
            Assert.That(dobottSzam, Is.EqualTo("P1 Nyert."), "Az első karakter győzelménél a dobott szám értéke megváltozik az első karakter nevére.");
        }

        [Test]
        public void MasodikKarakterGyozelmekorDobottSzamAKarakterNevereValtozik()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            elsoGomb.Click();

            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            driver.ExecuteScript("document.getElementById('dobottszam').innerHTML = '19';");
            masodikGomb.Click();

            var dobottSzam = driver.FindElement(By.Id("dobottszam")).Text;
            Assert.That(dobottSzam, Is.EqualTo("P2 Nyert."), "A második karakter győzelménél a dobott szám értéke megváltozik a második karakter nevére.");
        }

        [Test]
        public void GombokNyomasaUtanADobottEsAKiirtSzamValtozik()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            var dobottSzam = driver.FindElement(By.Id("dobottszam"));

            var dobottSzamok = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                elsoGomb.Click();
                var kezdoDobottSzam = int.Parse(dobottSzam.Text);

                masodikGomb.Click();
                var ujDobottSzam = int.Parse(dobottSzam.Text);

                dobottSzamok.Add(kezdoDobottSzam);
                dobottSzamok.Add(ujDobottSzam);

                Assert.That(ujDobottSzam, Is.Not.EqualTo(kezdoDobottSzam), "A kiírt számnak változnia kellene az újabb dobás után.");
            }

            var elteroDobottSzamok = dobottSzamok.Distinct().ToList();
            Assert.IsTrue(elteroDobottSzamok.Count > 1, "A dobott számnak változnia kellene az újabb dobás után.");
        }

        [Test]
        public void ElsoGombMegnyomasanalElsoKarakterMozgasa()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            var elsoKarakter = driver.FindElement(By.CssSelector(".elsmezo.p1"));

            elsoGomb.Click();
            var ujKarakterPozicio = driver.FindElement(By.CssSelector(".elsmezo.p1"));

            Assert.That(ujKarakterPozicio, Is.Not.EqualTo(elsoKarakter), "Az első gomb lenyomása után az első karakternek kellene mezőt váltania.");
        }

        [Test]
        public void MasodikGombMegnyomasanalMasodikKarakterMozgasa()
        {
            var elsoGomb = driver.FindElement(By.Id("elsogomb"));
            var masodikGomb = driver.FindElement(By.Id("masodikgomb"));
            var masodikKarakter = driver.FindElement(By.CssSelector(".elsmezo.p2"));

            elsoGomb.Click();
            masodikGomb.Click();
            var ujKarakterPozicio = driver.FindElement(By.CssSelector(".elsmezo.p2"));

            Assert.That(ujKarakterPozicio, Is.Not.EqualTo(masodikKarakter), "A második gomb lenyomása után a második karakternek kellene mezőt váltania.");
        }
    }
}