/*        public string Apply(string value) =>
            this.FirstOrDefault(x => x.Value == value)?
                .TranslatesTo ?? value;
*/
        public string Apply(string value)
        {
            string[] line = value.Split(' ');
            if (this.Exists(x=>x.Value == line[0]))
                line[0] = this.Find(x=>x.Value == line[0]).TranslatesTo;
            string rv = string.Join(" ", line);
            return rv;
        }
