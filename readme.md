
## 開發工具
 * Visual Studio 2017 Community 15.7.4
 
### 環境
 * Target Framework - .NET Framework 4.7
 * ASP.NET MVC 5.2.4
### 安裝套件
* Install-Package EntityFramework
* Install-Package Microsoft.Extensions.DependencyInjection
* Install-Package NLog
* Install-Package NLog.Config

###  目的
* include 一對多關聯資料表時，是否會有 join ?
    * 有 - CountryDAL.Get()
* insert / update 時，欄位為 mapping table id 是否會新增一筆新的資料至 mapping table 中 ?
    * insert 會 - CountryDAL.Add()
    * insert 不會 - CvDAL.Add()
    * update 不會 - CountryDAL.Update()
* TODO:雙主鍵 self reference one key

### 注意事項
* DI Scoped DbContext 時，當多次透過 Attach() 來更新資料時，會很容易出現 Exception。
    * 原因是第一次 Attach() 後，會把資料 cache 在 Local 中。進行第二次 Attach 時，就會發生衝突。
    * 測試過以下二種環境，但目前還不清楚實際原因。
        * 用 ASP.NET MVC + EF Core，同樣會有此狀況
        * 用 ASP.NET Core + EF Core + Attach() 無此狀況。
    * 目前試出的解決方式：
        1. 改用 AddOrUpdate() 進行更新資料的動作
        2. 每次更新都手動從資料庫取出資料、呼叫 Attach()，來進行更新資料
        3. 改用 DI Transient DbContext
        

## Demo


