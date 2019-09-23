namespace AxaFrance.Extensions.DependencyInjection.WCF.Sample
{
    internal class SampleDataProvider : IDataProvider
    {
        public void Dispose() => System.Diagnostics.Debug.WriteLine("disposal of IDisposable object");

        public string GetData(int value) => $"You entered: {value}";

        public string GetSuffix() => "Suffix";
    }
}