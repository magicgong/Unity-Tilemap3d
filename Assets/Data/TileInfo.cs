public class TileInfo
{
	public string name = "Default";
	public bool isWalkable = false;
	public bool blocksLineOfSight = false;
	public int typeId = 0;

	// Add member properties as needed.

	public TileInfo(string name, bool isWalkable, bool blocksLineOfSight, int typeId)
    {
		this.name = name;
		this.isWalkable = isWalkable;
		this.blocksLineOfSight = blocksLineOfSight;
		this.typeId = typeId;

		// Initialize member properties as needed.
	}

}
