# SAP_BASE
To caculate the performance-related pay of teacher of training base

# MYSQL  
多表查询：降序DESC 升序ASC。
```mysql
SELECT ss.date,ss.teacher,ss.session,s.name,s.type,ss.class,c.type,c.major,c.number,ss.`full/half`,d.day_of_week,d.holiday 
from sap_test.session_schedule ss
left join sap_test.class c on c.class=ss.class
left join sap_test.date d on d.date=ss.date
left join sap_test.session s on s.session=ss.session
where c.major='AV' AND s.type = '147' 
ORDER BY ss.date DESC，ss.teacher;
```  
指定日期段查询，参考[博客](https://www.cnblogs.com/zhangliang88/p/5479682.html)
```mysql
Select * From sap_test.session_schedule ss 
Where DATE_FORMAT(date,'%m-%d') >= '06-07' and DATE_FORMAT(date,'%m-%d') <= '06-12' 
ORDER BY ss.date,ss.session asc;
```

# 利用NPOI读写excel的方法  
1.利用NuGet方法安装NPOI  
2.读写excel可按照这两篇博客（两篇导出都为xsl）：  
[WPF通过NPIO读写Excel操作](https://www.cnblogs.com/lunawzh/p/5981492.html)，该篇读取时自动添加ABC等列表头，
且只能读取xsl。  
[使用NPOI导入导出Excel(xls/xlsx)数据到DataTable中](https://www.cnblogs.com/songrun/p/3547738.html)，该篇读取时
以excel文件的第一行作为列表头，且可读xsl和xslx文件。  
可按自己需求参照修改！
