   [HttpGet]
        public async Task<IActionResult> AsyncAwaitGetMethod()
        {
            var content = string.Empty;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:54451/api/Category");
                try
                {
                    var s = response.EnsureSuccessStatusCode();

                    content = await response.Content.ReadAsStringAsync();


                }
                catch (Exception ex)
                {

                }
            }
            return Ok(JsonConvert.DeserializeObject<Categorydto>(content));
        }