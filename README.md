# SAP_BASE
To caculate the performance-related pay of teacher of training base

# MYSQL
```mysql
SELECT ss.date,ss.teacher,ss.session,s.name,s.type,ss.class,c.type,c.major,c.number,ss.`full/half`,d.day_of_week,d.holiday 
from sap_test.session_schedule ss
left join sap_test.class c on c.class=ss.class
left join sap_test.date d on d.date=ss.date
left join sap_test.session s on s.session=ss.session
where c.major='AV' AND s.type = '147' 
ORDER BY ss.date DESC，ss.teacher;
```
降序DESC 升序ASC
