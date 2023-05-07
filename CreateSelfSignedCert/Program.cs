// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, Let's create self singed certificate!");

string subject = $"C=CN, O=Hugehardsoft, OU=R&D Hub, CN={Environment.MachineName} Sample";
//string randomPass = Convert.ToBase64String(RandomNumberGenerator.GetBytes(6));
//Console.WriteLine($"Please record the password: {randomPass} for exporting the certificate");

var rsa = RSA.Create();
var x500DistinguishedName = new X500DistinguishedName(subject);
var req =
    new CertificateRequest(x500DistinguishedName, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

SubjectAlternativeNameBuilder sanBuilder = new SubjectAlternativeNameBuilder();
sanBuilder.AddDnsName("localhost");
sanBuilder.AddDnsName(Environment.MachineName);
req.CertificateExtensions.Add(sanBuilder.Build());

var cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(10));
File.WriteAllBytes("C:\\temp\\SelfSignedSample.pfx", cert.Export(X509ContentType.Pfx, "123456"));

//X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine, OpenFlags.MaxAllowed);
//var removeCert = store.Certificates.FirstOrDefault(c => c.Subject == subject);
//if (removeCert != null)
//    store.Remove(removeCert);
//store.Add(cert);
//store.Close();

//var rawData = cert.RawData;
//using (var write = new StreamWriter(@"C:\temp\SelfSignedSample.crt"))
//{
//    write.WriteLine("-----BEGIN CERTIFICATE-----");
//    write.WriteLine(Convert.ToBase64String(rawData, Base64FormattingOptions.InsertLineBreaks));
//    write.WriteLine("-----END CERTIFICATE-----");
//}

//var privateKey = cert.GetRSAPrivateKey();
//if (privateKey != null)
//{
//    var keyData = privateKey.ExportRSAPrivateKey();
//    using (var write = new StreamWriter(@"C:\temp\SelfSignedSample.key"))
//    {
//        write.WriteLine("-----BEGIN RSA PRIVATE KEY-----");
//        write.WriteLine(Convert.ToBase64String(keyData, Base64FormattingOptions.InsertLineBreaks));
//        write.WriteLine("-----END RSA PRIVATE KEY-----");
//    }
//}

Console.WriteLine("Finish generate self signed certificate.");
Console.ReadKey();