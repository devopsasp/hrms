

SELECT 
		Emp_name as Name,
		CONVERT(date,Dates) Date,
		Days,
		CONVERT(nvarchar,intime,8)Intime,
		CONVERT(nvarchar,Late_in,8)Late_in,
		CONVERT(nvarchar,early_out,8)Early_out,
		CONVERT(nvarchar,outtime,8)Outtime,
		CONVERT(nvarchar,Late_out,8)Late_out,
		leave_code as Leave_Name
	FROM 
		time_card
	where 
		dates between '09/01/2019' and '09/30/2019' 
	and pn_companyid = '2' and  pn_BranchID = '13' 
	and emp_code = '05'
	order by dates



	