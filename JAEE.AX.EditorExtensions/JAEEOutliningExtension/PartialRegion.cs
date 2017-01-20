
namespace JAEE.AX.EditorExtensions
{
    class PartialRegion
    {
        public int StartLine { get; set; }
        public int StartOffset { get; set; }
        public int Level { get; set; }
        public PartialRegion PartialParent { get; set; }
        public string LineText { get; set; }
        public int TokenIndex { get; set; }
    }
}
