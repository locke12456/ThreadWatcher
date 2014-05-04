using System.Reflection;
using System.Runtime.CompilerServices;

//
// 組件的一般資訊是由下列的屬性集控制。
// 變更這些屬性的值即可修改組件的相關
// 資訊。
//
[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//
// 組件的版本資訊是由下列四項值構成:
//
//      主要版本
//      次要版本
//      修訂編號
//      組建編號
//
// 您可以指定所有的值，也可以依照以下的方式，使用 '*' 將修訂和組建編號
// 指定為預設值:

[assembly: AssemblyVersion("1.0.*")]

//
// 若要簽署組件，您必須指定要使用的金鑰。
// 如需組件簽署的詳細資訊，請參閱 Microsoft .NET Framework 文件。
//
// 請使用下列屬性控制要使用哪個金鑰進行簽署。
//
// 注意: 
//   (*) 如果沒有指定金鑰 - 將不會簽署組件。
//   (*) KeyName 是指已安裝在電腦上密碼編譯服務
//       提供者 (CSP) 中的金鑰。
//   (*) 如果同時指定了金鑰檔案和金鑰名稱，
//       將會發生下列的處理程序:
//       (1) 如果在 CSP 中可以找到 KeyName，就會使用此金鑰。
//       (2) 如果 KeyName 不存在而 KeyFile 存在，就會將 KeyFile 中的金鑰 
//           安裝到 CSP 中並加以使用。
//   (*) 延遲簽署是一個進階選項 - 如需詳細資訊，請參閱 Microsoft .NET Framework
//       文件。
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
