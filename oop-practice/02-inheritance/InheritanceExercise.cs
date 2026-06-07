namespace OopPractice.Inheritance;

// TASK: Inheritance — Catalogue Item Hierarchy
//
// You are modelling different types of items in a Product Catalogue.
// All items share common data, but each type has its own extra properties.
//
// Your job:
// 1. Create a base class `CatalogueItem` with:
//    - `Name` property (read-only, no empty/whitespace allowed)
//    - `Description` property (read-only)
//    - A `virtual` method `GetSummary()` returning: "[Name]: [Description]"
//
// 2. Create `PhysicalProduct : CatalogueItem` with:
//    - `WeightKg` property (must be > 0)
//    - `override GetSummary()` — call base.GetSummary() and append "(Weight: Xkg)"
//
// 3. Create `DigitalProduct : CatalogueItem` with:
//    - `DownloadUrl` property (must not be empty/whitespace)
//    - `override GetSummary()` — call base.GetSummary() and append "(Download: [url])"
//
// CONSTRAINTS:
//   - Derived constructors MUST call base(...) — do NOT repeat Name/Description validation
//   - GetSummary() overrides MUST call base.GetSummary() — do NOT rebuild from scratch
//   - WeightKg must be validated in PhysicalProduct's constructor
//   - DownloadUrl must be validated in DigitalProduct's constructor
//
// HINT: Think about what belongs in the base class vs what each subclass owns exclusively.

// Write your code below this line:

public class CatalogueItem
{
    public string Name { get; }
    public string Description { get; }

    public CatalogueItem(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.");
        }

        Name = name;
        Description = description;
    }

    public virtual string GetSummary()
    {
        return $"{Name}: {Description}";
    }
}

public class PhysicalProduct : CatalogueItem
{
    public decimal WeightKg { get; }

    public PhysicalProduct(string name, string description, decimal weightKg)
        : base(name, description)
    {
        if (weightKg <= 0)
        {
            throw new ArgumentException("Weight must be greater than 0.");
        }

        WeightKg = weightKg;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} (Weight: {WeightKg}kg)";
    }
}

public class DigitalProduct : CatalogueItem
{
    public string DownloadUrl { get; }

    public DigitalProduct(string name, string description, string downloadUrl)
        : base(name, description)
    {
        if (string.IsNullOrWhiteSpace(downloadUrl))
        {
            throw new ArgumentException("Download URL cannot be null or empty.");
        }

        DownloadUrl = downloadUrl;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} (Download: {DownloadUrl})";
    }
}