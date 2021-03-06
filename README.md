# Unity-ExcelExporter
## 进度: 3%  

Excel表格导出工具

<p align="center" >
<img src="https://github.com/kaclok/Unity-ExcelExporter/tree/main/Tools/ExcelExporterUI/Resources/Desc.png" alt="ExporterUI" title="ExporterUI">
</p>

0. 无需安装office环境	
1. Excel表格打开时依然可以本程序读取		
2. 支持导出纯数据文件 以及 数据类定义文件		
	数据文件: 支持Json/Lua/Binary/Sqlite			
	数据类定义文件: C++/C# [想增加更多类型，修改源码即可]									
3. 支持自定义类型, 支持枚举, 支持数组，支持浮点数[为了规避帧同步问题]		
4. 支持Server/Client双端导出, 支持双端所需字段导出		
5. 支持一表多Sheet, 支持增量/全量导出		
6. 支持svn的快捷回滚，快捷更新和快捷上传			


在考虑：		
1. 是否支持表格间继承， 比如FormPlayer和FormNPC继承FormEntity		
2. 是否支持简单的表格外键引用检测		
3. 考虑了类定义的内存对齐原理，多个bool类型集成为一个uint处理，节约内存	
4. 考虑了表格子段默认值的内存占用		

关于导表的讨论: https://zhuanlan.zhihu.com/p/146320103		

参考：		
1. https://github.com/kaclok/HiExcel2Protobuf    
2. https://github.com/kaclok/FlashExcel    
3. https://github.com/yimengfan/BDFramework.Core    
4. https://github.com/kaclok/Excel-Translator    
5. https://github.com/monkey256/ExcelExport    
6. https://github.com/xieliujian/Excel2Unity    
7. https://github.com/sniper00/ExcelExport 
8. https://github.com/cheadaq/tabugen
9. https://github.com/davyxu/tabtoy
10. https://www.zhihu.com/column/xieliujian   
11. https://www.cnblogs.com/msxh/p/8539108.html
