项目结构:

samples : 案例工程文件夹
src: 类库源码文件夹
test/benchmark : 性能测试文件夹
test/ut : 单元测试文件夹
solution items: 全局配置文件夹, 主要存放配置文件.

当创建完工程时需要手动更改项目名称.




Directory.Build.props 文件:

作用范围: src 文件夹下的项目;

区域说明:

	1. 框架配置区:
		a. TargetFrameworks : 运行时/标准库兼容版本标签, 指明了 src 下项目所兼容的框架, 如果需要定制化,可移除该标签,并在 src 下的项目工程文件中自定义框架目标.
		b. Nullable : 可空引用标签, 该标签默认开启, 请养成严谨编程的好习惯.
		c. LangVersion : 语言版本标签,默认使用预览版.

	2. 打包信息区域:
		a. IsPackable 是否开启打包功能.
		b. IncludeSymbols 是否包含元数据符号等.
		c. GeneratePackageOnBuild 是否在项目生成时生成项目包.
		d. PackageRequireLicenseAcceptance 是否需要接入许可证.
		e. PackageProjectUrl 项目链接地址.
		f. Copyright 作者及合作者等版权说明.
		g. Company 该包所属团体
		h. Authors 作者昵称/姓名
		i. PackageIcon 上传到 NUGET 时,包所用的头像. 这里会从[资源区]链接过来.
		j. PackageLicenseFile 上传到 NUGET 时,包所包含的许可证文件, 这里会从[资源区]链接过来.

	3. 资源输出区:
		a. 第一行: {myImage} 需要替换成 resource 文件夹中你所存放的,打算给包上传的图片文件.
		b. 第一行: {nuget pack image} 需要替换成[打包信息区]中你所指定的要上传的 PackageIcon 节点值. 该字段类似于软连接了资源文件.
		c. 第二行: {myLicense} 参考 a.b 两点, 原理相同.




Project 工程文件:

配置说明:
	
	1. PackageId : nuget 包名称.
	2. PackageReleaseNotes : nuget 包发版信息.
	3. PackageTags : nuget 检索关键字.
	4. Description : nuget 项目描述信息.
	5. Version : nuget 包版本信息.
	6. FileVersion : nuget 文件版本信息.
	7. AssemblyVersion : nuget 生成程序集的版本信息.




publish yml 文件:

该文件为 GITHUB 管道中的发布流程配置. 建议放置在 Main 分支下. 
配置说明:

	Push to Nuget - Project 节点: ${{ secrets.NUGET_KEY }} 为 GITHUB 环境变量中存储的发布 NUGET 所需要的 KEY.




test.yml 文件:

该文件为 GITHUB 管道中的测试流程配置. 建议放置在 test 分支下. 
配置说明:

	第一行 : {myProject} 修改为自己项目的名称.
	Upload to Codecov 节点: ${{ secrets.COVERAGE_KEY }} 为 推送到 测试覆盖率 网站所需要的 KEY.


