
CREATE PROCEDURE spr_role_retrieve
AS
BEGIN
	   
		SET NOCOUNT ON		
			SELECT 
				ROW_NUMBER() OVER(ORDER BY pn_DepartmentID ASC) AS row_no,
				pn_DepartmentID as role_id,
				v_DepartmentName as role_name				
			FROM 
				paym_department
			WHERE
				status='Y'
			ORDER BY
				v_DepartmentName
				       
		SET NOCOUNT OFF
END