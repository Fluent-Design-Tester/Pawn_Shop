namespace Pawn_Shop.Dto
{
    class PawnType
    {
        public int type_id { get; set; }

        public string name { get; set; }

        public string short_name { get; set; }

        public int category_id { get; set; }

        public int display_no { get; set; }

        public PawnType(int typeId, string name, string shortName, int displayNo)
        {
            this.type_id = typeId;
            this.display_no = displayNo;
            this.name = name;
            this.short_name = shortName;
        }
    }
}
