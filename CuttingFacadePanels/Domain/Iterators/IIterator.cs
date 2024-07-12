namespace CuttingFacadePanels
{
	public interface IIterator
	{
		public object First();
		public object Next();
		public bool IsDone();
		public object CurrentItem();
	}
}