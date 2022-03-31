      [HttpGet]
        public async Task<string> AsyncAwaitGetMethod()
        {
            string content = string.Empty;

            await Task.Run(
                () =>
                {
                    var k = System.IO.File.ReadAllLines(@"H:\10.Interview\Code Samples\codes\C#\FILES\StockPrices_Small.csv");
                    content = string.Join("/", k);
                }
                );

            return "Kog" + content;
        }