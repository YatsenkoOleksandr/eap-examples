namespace DomainModel;

internal class Product
{
  public string Name { get; private set; }

  private RecognitionStrategy recognitionStrategy;

  public Product(string name, RecognitionStrategy recognitionStrategy)
  {
    Name = name;
    this.recognitionStrategy = recognitionStrategy;
  }

  public static Product NewWordProcessor(string name)
  {
    return new Product(name, new CompleteRecognitionStrategy());
	}

	public static Product NewSpreadsheet(string name)
  {
    return new Product(name, new ThreeWayRecognitionStrategy(60, 90));
	}

	public static Product NewDatabase(string name)
  {
    return new Product(name, new ThreeWayRecognitionStrategy(30, 60));
	}

  public void CalculateRevenueRecognitions(Contract contract)
  {
    recognitionStrategy.CalculateRevenueRecognitions(contract);
  }
}