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
指定日期段查询，参考[博客]（https://www.cnblogs.com/zhangliang88/p/5479682.html）
```mysql
Select * From sap_test.session_schedule ss 
Where DATE_FORMAT(date,'%m-%d') >= '06-07' and DATE_FORMAT(date,'%m-%d') <= '06-12' 
ORDER BY ss.date,ss.session asc;
```
