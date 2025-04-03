using Newtonsoft.Json;

namespace NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ProductMasterDTO
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("stockKeepingUnit")]
        public string StockKeepingUnit { get; set; }

        [JsonProperty("itemGroup")]
        public string ItemGroup { get; set; }

        [JsonProperty("useCountryOfOrigin")]
        public bool UseCountryOfOrigin { get; set; } = true;

        [JsonProperty("useLotNo")]
        public bool UseLotNo { get; set; }

        [JsonProperty("mandatoryLotNo")]
        public bool MandatoryLotNo { get; set; }

        [JsonProperty("mandatoryCountryOfOrigin")]
        public bool MandatoryCountryOfOrigin { get; set; } = true;

        [JsonProperty("mandatoryCheckItemOrSSCC")]
        public bool MandatoryCheckItemOrSSCC { get; set; } = true;

        [JsonProperty("codeOfGoods")]
        public string CodeOfGoods { get; set; }

        [JsonProperty("reference1")]
        public string Reference1 { get; set; }

        [JsonProperty("reference2")]
        public string Reference2 { get; set; }

        [JsonProperty("reference3")]
        public string Reference3 { get; set; }

        [JsonProperty("reference4")]
        public string Reference4 { get; set; }

        [JsonProperty("isStockable")]
        public bool IsStockable { get; set; } = true;

        [JsonProperty("adrGroup")]
        public string AdrGroup { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("design")]
        public string Design { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("configDetails")]
        public List<KtnProductConfigDetails> ConfigDetails { get; set; }

        [JsonProperty("barcodes")]
        public List<KtnBarCodes> BarCodes { get; set; }

        [JsonProperty("comments")]
        public List<KtnProductComments> Comments { get; set; }

        [JsonProperty("configurations")]
        public List<KtnProductConfigurations> Configurations { get; set; }

        public ProductMasterDTO()
        {

        }

        public ProductMasterDTO(ProductMasterDTO other)
        {
            Customer = other.Customer;
            Code = other.Code;
            Description = other.Description;
            StockKeepingUnit = other.StockKeepingUnit;
            ItemGroup = other.ItemGroup;
            UseCountryOfOrigin = other.UseCountryOfOrigin;
            UseLotNo = other.UseLotNo;
            MandatoryLotNo = other.MandatoryLotNo;
            MandatoryCountryOfOrigin = other.MandatoryCountryOfOrigin;
            MandatoryCheckItemOrSSCC = other.MandatoryCheckItemOrSSCC;
            CodeOfGoods = other.CodeOfGoods;
            Reference1 = other.Reference1;
            Reference2 = other.Reference2;
            Reference3 = other.Reference3;
            Reference4 = other.Reference4;
            IsStockable = other.IsStockable;
            AdrGroup = other.AdrGroup;
            Model = other.Model;
            Design = other.Design;
            Size = other.Size;
            Color = other.Color;
            Season = other.Season;
            ConfigDetails = other.ConfigDetails;
            BarCodes = other.BarCodes;
            Comments = other.Comments;
            Configurations = other.Configurations;
        }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KtnProductComments
    {
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("lineReference")]
        public string LineReference { get; set; } = "1";

        [JsonProperty("showOnInbound")]
        public bool ShowOnInbound { get; set; } = true;

        [JsonProperty("showOnOutbound")]
        public bool ShowOnOutbound { get; set; } = true;
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KtnProductConfigDetails
    {
        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("configuration")]
        public string Configuration { get; set; }

        [JsonProperty("qtySKUPerUnit")]
        public int QtyskuPerUnit { get; set; }

        [JsonProperty("isRFUnit")]
        public bool IsRFUnit { get; set; }

        [JsonProperty("isStockHolder")]
        public bool IsStockHolder { get; set; }

        [JsonProperty("isMinPickQty")]
        public bool IsMinPickQty { get; set; }

        [JsonProperty("fullHolderPicking")]
        public bool FullHolderPicking { get; set; }

        [JsonProperty("timeRegIn")]
        public bool TimeRegIn { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KtnProductConfigBarcodes
    {
        [JsonProperty("barcode")]
        public string BarCode { get; set; }

        [JsonProperty("qtySKUPerUnit")]
        public string SKUQtyPerUnit { get; set; }

        [JsonProperty("configuration")]
        public string Configuration { get; set; } = "CONFIG1";

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KtnProductDetails
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KtnBarCodes
    {
        [JsonProperty("barcode")]
        public string BarCode { get; set; }

        [JsonProperty("isEAN")]
        public bool isEAN { get; set; }

        /*        [JsonProperty("isSSCC")]
                public bool isSSCC { get; set; }*/
    }

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class KtnProductConfigurations
    {
        [JsonProperty("configuration")]
        public string Configuration { get; set; } = "CONFIG1";

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; } = true;

        [JsonProperty("stackHeight")]
        public int StackHeight { get; set; }

        [JsonProperty("typeMover")]
        public string TypeMover { get; set; } = "NEW";
    }
}
