using System;

namespace InkCartridge
{
    public class Cartridge
    {
        public int IdCartridge { get; set; }
        public int IdType { get; set; }
        public TypeCartridge TypeCartridge { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallationDate { get; set; }
        public int IdStatus { get; set; }
        public Status Status { get; set; }
        public string Comment { get; set; }
    }
}
