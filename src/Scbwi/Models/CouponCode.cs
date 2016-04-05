namespace Scbwi.Models {
    public class CouponCode {
        public int id { get; set; }
        public CodeType type { get; set; }
        public decimal value { get; set; }
        public int numuses { get; set; }
        public string text { get; set; }
    }

    public enum CodeType {
        Percent,
        Total,
        Critique
    }
}