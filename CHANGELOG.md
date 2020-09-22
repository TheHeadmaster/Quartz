# Changelog

<a name="1.2.1-alpha"></a>
## 1.2.1-alpha (2020-09-22)

### Optimization

- ♻️ Remove and refactor a metric shit-ton of boilerplate code [[69f7e03](https://github.com/TheHeadmaster/Quartz/commit/69f7e031df2d2e90c584ce510abbbd96549cc7f2)]
- ♻️ Preferences now save to the user&#x27;s AppData directory instead of directly inside the Program Files folder [[609d511](https://github.com/TheHeadmaster/Quartz/commit/609d511f626c70ab2d7946ff75e4dfb2961d09ca)]
- ♻️ Move Logs output directory from Program Files to AppData [[7dc0c43](https://github.com/TheHeadmaster/Quartz/commit/7dc0c43e3ca19f4d4997224a42cef9cc6ac9c099)]
- ♻️ Common functionality between Quartz.IDE and Quartz.Engine moved to Quartz.Core [[be3afff](https://github.com/TheHeadmaster/Quartz/commit/be3afff9a840c2c8663ab2039691c4674fd83e95)]

### Breaking and Major Changes

- 💥 Project files are now in Database format instead of Json file format [[764803c](https://github.com/TheHeadmaster/Quartz/commit/764803cd13508fdbf1c89a5ae1b9419eb5228a5c)]
- 💥 Previous projects are now incompatible moving forward, starting with 1.2.1 [[6fafdf3](https://github.com/TheHeadmaster/Quartz/commit/6fafdf35d357ef65683dd6c16e97b9a1e112430d)]
- 🏗️ Add Quartz.Testing Unit Test Project [[7f867f4](https://github.com/TheHeadmaster/Quartz/commit/7f867f47734ee21e6db395c950ce515f0f5eff9e)]
- 🏗️ Add Quartz.Engine Library [[e764051](https://github.com/TheHeadmaster/Quartz/commit/e764051cf313a714ae3cac8cbd2672f128e4cb04)]
- 🚀 Change Quartz Editor to Quartz IDE [[fdfb673](https://github.com/TheHeadmaster/Quartz/commit/fdfb673aefe93252bb2d21b84cffbbd41840998f)]

### Bug Fixes

- 🐛 Selecting No to opening a new project while an existing unsaved project was open still opened the project and didn&#x27;t save. Press F to pay respects [[46dba05](https://github.com/TheHeadmaster/Quartz/commit/46dba05e9d7c4a9bfc1c79ae66cf9dd5e307a3b2)]

### New Features

- ✨ Add backwards compatibility check for older projects, and versioning for projects moving forward [[03b4966](https://github.com/TheHeadmaster/Quartz/commit/03b4966b2d30ac6139d5602ac518228d2c222722)]

### Diagnostics

- ⚗️ Add method-level logging and error catching utilizing Serilog and MethodBoundaryAspect [[ab97e2f](https://github.com/TheHeadmaster/Quartz/commit/ab97e2fa8e178bd3f8e95c4ea2668948ebfdaf50)]

### Dependency Changes

- ➕ Add Microsoft.CodeAnalysis.CSharp.Workspaces Compiler Library [[31525e4](https://github.com/TheHeadmaster/Quartz/commit/31525e438f4983bae0eaefcfb5d6b385cd9ab32e)]
- ➕ Add Microsoft.CodeAnalysis Compiler Library [[163612c](https://github.com/TheHeadmaster/Quartz/commit/163612c5a493c5ce9155937661140faa490f7cdd)]
- ➕ Add Microsoft.Build.Tasks.Core Compiler Library [[799113b](https://github.com/TheHeadmaster/Quartz/commit/799113b98be17deae5b2801aae314b317083593c)]
- ➕ Add Microsoft.Build.Locator Compiler Library [[7a10c60](https://github.com/TheHeadmaster/Quartz/commit/7a10c608ced68ff2e40f6743ef19254807de8683)]
- ➕ Add Microsoft.Build.Framework Compiler Library [[376c473](https://github.com/TheHeadmaster/Quartz/commit/376c4732c627cbcde6ed34c6957cdbca92411f49)]
- ➕ Add Microsoft.Build.Runtime Compiler Library [[51efa89](https://github.com/TheHeadmaster/Quartz/commit/51efa8993dbe3b1bef5f22f96939f82c312956ba)]
- ➕ Add Microsoft.Build.Engine Compiler Library [[7c72459](https://github.com/TheHeadmaster/Quartz/commit/7c724592a0fe3cd0a6c886e1f125c7a4cefb1d56)]
- ➕ Add Microsoft.Build Compiler Library [[29d06f6](https://github.com/TheHeadmaster/Quartz/commit/29d06f678ec5271a88c9d211a69012e517b58c76)]
- ⬆️ Update Librarium 1.0.2 ➡️ 1.0.3 [[27e4ffc](https://github.com/TheHeadmaster/Quartz/commit/27e4ffcc71830e594aa5a2529ce5dd3dee1a12b6)]
- ➕ Add PersistentEntity Database Library [[f16ceb7](https://github.com/TheHeadmaster/Quartz/commit/f16ceb7bde0954c33f627831c16f6c87ba151b88)]
- ➕ Add FluentAssertions Testing Library [[f42ffa1](https://github.com/TheHeadmaster/Quartz/commit/f42ffa1550bf1ba14d7b1ac9ecfacba9a8ca3830)]
- ➕ Add XBehave Testing Library [[6ca9c8b](https://github.com/TheHeadmaster/Quartz/commit/6ca9c8b305edfdb966823104807d9d540038e962)]
- ⬆️ Update XUnit Visual Studio Runner 2.4.0 ➡️ 2.4.3 [[0506e07](https://github.com/TheHeadmaster/Quartz/commit/0506e0758e62a0abd8e43f323a664e340701e7e6)]
- ⬆️ Update XUnit 2.4.0 ➡️ 2.4.1 [[a9826d3](https://github.com/TheHeadmaster/Quartz/commit/a9826d378a2ae457e815b44e6b972d584de1339d)]
- ⬆️ Update .NET Test SDK 16.5.0 ➡️ 16.7.1 [[0d66e8d](https://github.com/TheHeadmaster/Quartz/commit/0d66e8dad08363e5c41107ac9d2f022f109a08d5)]
- ⬆️ Update Coverlet.Collector 1.2.0 ➡️ 1.3.0 [[f6c6c0c](https://github.com/TheHeadmaster/Quartz/commit/f6c6c0caea858f679fe6aeee95334c65ec24954d)]
- ➕ Add Serilog Analyzers [[560d15c](https://github.com/TheHeadmaster/Quartz/commit/560d15c506ff41e7fe6e4a923c1be285010069e6)]
- ➕ Add Serilog Compact Formatting Logging Library [[4c036a9](https://github.com/TheHeadmaster/Quartz/commit/4c036a987785600d983c506644ba4ba0f520e127)]
- ➕ Add Serilog Exception Logging Library [[355b7ad](https://github.com/TheHeadmaster/Quartz/commit/355b7ad47ede98a1736197d57193db7b3d32d0cc)]
- ➕ Add Serilog Seq Sink Library [[87a8b9f](https://github.com/TheHeadmaster/Quartz/commit/87a8b9fb8f50791b19b0f8fd160192c5e7e53137)]
- ➕ Add Serilog File Sink Library [[c913daa](https://github.com/TheHeadmaster/Quartz/commit/c913daa61ce5eeb4bd126adfd2285980d0348c28)]
- ➕ Add Serilog Console Sink Library [[3edad91](https://github.com/TheHeadmaster/Quartz/commit/3edad910758b4cde528eda27a3e6abd50c863d05)]
- ➕ Add Serilog Debug Sink Library [[49a95ba](https://github.com/TheHeadmaster/Quartz/commit/49a95ba462761db9ce5101b9426d4bd35fd659f3)]
- ➕ Add Serilog Logging Library [[48b5bcf](https://github.com/TheHeadmaster/Quartz/commit/48b5bcf6780bd0d3c4f9e2c7c33f755765094e21)]
- ⬆️ Upgrade Librarium and Librarium.WPF to Nuget package [[4b82008](https://github.com/TheHeadmaster/Quartz/commit/4b82008d5f40c0ad3f0f74c9dc17846b17d42ffe)]




<a name="1.1.6-alpha"></a>
## 1.1.6-alpha (2020-09-20)

### Optimization

- 🔥 Turns off Automatic Updating

### Breaking and Major Changes

- 📦 Final Release for 1.1 Alpha

<a name="1.1.5-alpha"></a>
## 1.1.5-alpha (2020-09-20)

### Optimization

- ♻️ Refactor various switch case statements [[2289d1e](https://github.com/TheHeadmaster/Quartz/commit/2289d1e96455269e0773a1ce36ac68cecad4991e)]

### Bug Fixes

- 🐛 JFile objects throw an error on certain null values [[6a16211](https://github.com/TheHeadmaster/Quartz/commit/6a16211af140a32cb62dac6e52c9b4b31dda273a)]
- 🐛 AvolonEdit chokes on an empty value [[b38355d](https://github.com/TheHeadmaster/Quartz/commit/b38355de9dcec81a4798701bed402fbc411bdbcb)]

<a name="1.1.4-alpha"></a>
## 1.1.4-alpha (2020-09-20)

### Diagnostics

- ⚗️ Test automatic upgrading when new versions are released [[9f45723](https://github.com/TheHeadmaster/Quartz/commit/9f45723cbf3f55093bc6a7bf973964d30ea36867)]

<a name="1.1.3-alpha"></a>
## 1.1.3-alpha (2020-09-20)

### Bug Fixes

- 🐛 Opening the Preferences window crashes Quartz [[91cf400](https://github.com/TheHeadmaster/Quartz/commit/91cf400f763aadb2d619f7bcfc686e37caadf464)]

### New Features

- ✨ Add an option in the Preferences menu to choose between Metric and Imperial measurement units [[ea95d36](https://github.com/TheHeadmaster/Quartz/commit/ea95d36fe433ee46d472d62e62d4ae89187f2131)]
- ✨ Height and weight values now display the unit of measurement inside the inputs [[46b549b](https://github.com/TheHeadmaster/Quartz/commit/46b549be8e2dd20f4e92303445e1994220a40d58)]

### Visual Changes

- 💄 Hook up the Preferences window to the main menu [[e060c88](https://github.com/TheHeadmaster/Quartz/commit/e060c88386130cae2ed8e5c07b96c31d876dd795)]

<a name="1.1.2-alpha"></a>
## 1.1.2-alpha (2020-09-19)

### Bug Fixes

- 🐛 Update Manager throws when checking for updates [[a52d98b](https://github.com/TheHeadmaster/Quartz/commit/a52d98b5d761e690862d711f287fdb349022b707)]

<a name="1.1.1-alpha"></a>
## 1.1.1-alpha (2020-09-18)

### Optimization

- ♻️ General cleanup and code standardization [[00e353d](https://github.com/TheHeadmaster/Quartz/commit/00e353d6fd3b4e13d9f2732ce98d5046ea363395)]

### Breaking and Major Changes

- 🏗️ Add Quartz.IDE Application [[7fc4c35](https://github.com/TheHeadmaster/Quartz/commit/7fc4c350a2404d6c3991858bed1aeb02de1cc0d1)]
- 🏗️ Add Installer Project [[8a1fdce](https://github.com/TheHeadmaster/Quartz/commit/8a1fdce1e30be1b30f6aa1196f1a8cfd839b0b57)]

### New Features

- ✨ Add a command to view the help window [[0308f2d](https://github.com/TheHeadmaster/Quartz/commit/0308f2d4a2af1d32310dab0f5c97d71a7691df96)]
- ✨ Add a command to check for updates [[c99efd9](https://github.com/TheHeadmaster/Quartz/commit/c99efd9006094e0849c0508f3e49af49b56fa52e)]
- ✨ Check for and download updates on startup [[7a6804f](https://github.com/TheHeadmaster/Quartz/commit/7a6804f425f3e507e82cb7dc82c3fbd71007b53a)]

### Visual Changes

- 💄 Hook up the About window to the main menu [[e3f4812](https://github.com/TheHeadmaster/Quartz/commit/e3f4812185c44c2baeca062d95681db23728d726)]
- 💄 Clean up UI for New Project window [[33b8776](https://github.com/TheHeadmaster/Quartz/commit/33b87767c9744ed313bbb8b405f21a80fe4ed898)]

### Dependency Changes

- ➕ Add Librarium WPF UI Library [[8abff3d](https://github.com/TheHeadmaster/Quartz/commit/8abff3db119c209f3f774049866b96467cb88b49)]
- ➕ Add Librarium Code Library [[a7f5c73](https://github.com/TheHeadmaster/Quartz/commit/a7f5c7353f08da9188254eae50a9185e100d91b8)]
- ➕ Add ReactiveUI WPF Events UI Library [[7d6243a](https://github.com/TheHeadmaster/Quartz/commit/7d6243a7d9909f16c8faa13ca831e8149e24c991)]
- ➕ Add ReactiveUI Fody Intermediate Language Weaver [[e6a20c9](https://github.com/TheHeadmaster/Quartz/commit/e6a20c9f5675c0715b14bedf58e4ee3b3d92b786)]
- ➕ Add ReactiveUI Validation UI Library [[91a5134](https://github.com/TheHeadmaster/Quartz/commit/91a513491bcd685ad9b2559a363bf9fb7338f63c)]
- ➕ Add ReactiveUI WPF UI Library [[0b76683](https://github.com/TheHeadmaster/Quartz/commit/0b7668342c88846228bb0bf8331949a6335e5dc1)]
- ➕ Add ReactiveUI UI Library [[8ef6919](https://github.com/TheHeadmaster/Quartz/commit/8ef691931c45849eb1f8e8413494ba744ef36581)]
- ➕ Add PropertyChanged Intermediate Language Weaver [[86ebb44](https://github.com/TheHeadmaster/Quartz/commit/86ebb44cf4039a8b80e642268fbec2204fd178db)]
- ➕ Add Newtonsoft.Json IO Library [[c95387c](https://github.com/TheHeadmaster/Quartz/commit/c95387cfce598426537fd284fb77a9a856b451ac)]
- ➕ Add WPF Interactivity UI Library [[6329c32](https://github.com/TheHeadmaster/Quartz/commit/6329c322de8e8355e1446ec34f693961ef51fb2f)]
- ➕ Add WindowsAPICodePack UI Library [[852b8bd](https://github.com/TheHeadmaster/Quartz/commit/852b8bd83958379a85d18b67c711148fcd1dfb77)]
- ➕ Add MethodBoundaryAspect Intermediate Language Weaver [[27360ac](https://github.com/TheHeadmaster/Quartz/commit/27360ac8d2d07b9d3b6f7cccc91729a9a1730603)]
- ➕ Add FontAwesome5 UI Library [[e56f479](https://github.com/TheHeadmaster/Quartz/commit/e56f4795920b13495d91f694c0ada430d3194503)]
- ➕ Add Fody Intermediate Language Weaver [[8333d6e](https://github.com/TheHeadmaster/Quartz/commit/8333d6edadc531d2f7c3128362e3776d05d68135)]
- ➕ Add FluentValidation UI Library [[6041cf6](https://github.com/TheHeadmaster/Quartz/commit/6041cf6e3c514a8b7d629cf6a521fb1a2c8d71bf)]
- ➕ Add Extended WPF Toolkit UI Library [[2682e60](https://github.com/TheHeadmaster/Quartz/commit/2682e604c06ce62b486aee1fe85512df1a6050c5)]
- ➕ Add AvalonDock UI Library [[b8626e0](https://github.com/TheHeadmaster/Quartz/commit/b8626e04ae4e8f86cf0d4d80b68cbdae20a6f6a9)]
- ➕ Add AvalonEdit UI Library [[6bdb796](https://github.com/TheHeadmaster/Quartz/commit/6bdb79699145e98c048df4b911aa3e777574a45d)]
- ➕ Add Octokit.Reactive [[7a6804f](https://github.com/TheHeadmaster/Quartz/commit/7a6804f425f3e507e82cb7dc82c3fbd71007b53a)]

<a name="1.0.0-alpha"></a>
## 1.0.0-alpha (2020-09-17)

### Breaking and Major Changes

- 🏗️ Add Quartz.Core Library [[bee3bac](https://github.com/TheHeadmaster/Quartz/commit/bee3bac04683c103a4b22d2857299f7f7cc31bde)]
- 🎉 Open Quartz repository [[ca1af7d](https://github.com/TheHeadmaster/Quartz/commit/ca1af7d308f9fd18e6d69e6d6be4778af03841bf)]

### Visual Changes

- 🍱 Add Quartz.png to solution [[cbd78ce](https://github.com/TheHeadmaster/Quartz/commit/cbd78cef1e93c6831accc45e534231ab03f4ecd3)]

### Diagnostics

- ⚗️ Test automatic changelog generation [[4edf583](https://github.com/TheHeadmaster/Quartz/commit/4edf5839ce391c381acc36fc8ae041e927d8992b)]

### Documentation

- 📝 Add Code Owners file [[aacae69](https://github.com/TheHeadmaster/Quartz/commit/aacae69c5a7efddb9189ef28f2a4760164fe6545)]
- 📝 Add Code of Conduct file [[443265e](https://github.com/TheHeadmaster/Quartz/commit/443265ec4fe5bcff41c3ec807f6c75d61eaa3f62)]
- 📝 Add badges and description for Readme file [[dcc084f](https://github.com/TheHeadmaster/Quartz/commit/dcc084fbac989a0e2daeca5da5334bb29bf683ad)]
- 📝 Add README file [[081b185](https://github.com/TheHeadmaster/Quartz/commit/081b1856aeb687ff73ced92d868f47ebf154f4a2)]
- 📝 Add Changelog file [[d5a6f07](https://github.com/TheHeadmaster/Quartz/commit/d5a6f07dbbec74de99b8c7201a9e889ae0acec8f)]
- 📄 Add MIT license [[253567f](https://github.com/TheHeadmaster/Quartz/commit/253567fec2affb543cfa3d57a19fc8bba83f3e3d)]
- 📝 Add Funding information [[f74e37c](https://github.com/TheHeadmaster/Quartz/commit/f74e37cbe317980e6ee54ffc4af92d234214b31a)]
- 📝 Add Issue templates [[0ae3990](https://github.com/TheHeadmaster/Quartz/commit/0ae39904af976b13c674ff895976661f46a44018)]

### Miscellaneous

- 🔧 Add Travis CI configuration file [[94a9104](https://github.com/TheHeadmaster/Quartz/commit/94a91047b2bba7201e2ab025733457f71e7e7cca)]
- 🔧 Add Quartz solution file [[b5acbc7](https://github.com/TheHeadmaster/Quartz/commit/b5acbc71c6fc980fb4cbc78a8f74aae25fecf87f)]
- 🔧 Add Gitmoji Changelog configuration file [[fed71e4](https://github.com/TheHeadmaster/Quartz/commit/fed71e4c80a7c68401061c246641903ec1bcbcde)]
- 🔧 Add Visual Studio editor configuration file [[814e5c9](https://github.com/TheHeadmaster/Quartz/commit/814e5c91f7757b14c2a9625d3168efed9af43827)]
