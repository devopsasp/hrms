
GO
/****** Object:  StoredProcedure [dbo].[spr_role_acess_retrieve]    Script Date: 01-08-2019 19:44:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE spr_role_acess_retrieve
(
	@i_a_role_id	VARcHAR(50)  = null
)
AS 
BEGIN
		
		IF(@i_a_role_id IS NULL)
		BEGIN
			SELECT 
				PK_Menu_Id,
				Menu_Name,
				CASE WHEN menu.View_Status IS NULL THEN CONVERT(BIT,0) WHEN menu.View_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS view_visible,
				CASE WHEN menu.Save_Status IS NULL THEN CONVERT(BIT,0) WHEN menu.Save_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS save_visible,
				CASE WHEN menu.Delete_Status IS NULL THEN CONVERT(BIT,0) WHEN menu.Delete_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS delete_visible,
				CONVERT(BIT,0) AS view_checked,
				CONVERT(BIT,0) AS save_checked,
				CONVERT(BIT,0) AS delete_checked

			FROM 
				tbl_Menu menu								
			WHERE
				Select_Field	= 1 AND
				Menu_Access		= 1
			ORDER BY
				Menu_Code ASC				
		END
		ELSE
		BEGIN
			SELECT 
				menu.PK_Menu_Id,
				menu.Menu_Name,
				CASE WHEN menu.View_Status IS NULL THEN CONVERT(BIT,0) WHEN menu.View_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS view_visible,
				CASE WHEN menu.Save_Status IS NULL THEN CONVERT(BIT,0) WHEN menu.Save_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS save_visible,
				CASE WHEN menu.Delete_Status IS NULL THEN CONVERT(BIT,0) WHEN menu.Delete_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS delete_visible,
				CASE WHEN acc.View_Status IS NULL THEN CONVERT(BIT,0) WHEN acc.View_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS view_checked,
				CASE WHEN acc.Save_Status IS NULL THEN CONVERT(BIT,0) WHEN acc.Save_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS save_checked,
				CASE WHEN acc.Delete_Status IS NULL THEN CONVERT(BIT,0) WHEN acc.Delete_Status = 0 THEN CONVERT(BIT,0) ELSE CONVERT(BIT,1) END AS delete_checked
					FROM 
				tbl_Menu menu
				LEFT JOIN tbl_Role_Access acc ON menu.PK_Menu_Id = acc.FK_Menu_Id AND acc.FK_Role_Id = @i_a_role_id AND Active = 1
			WHERE
				menu.Select_Field		= 1	AND
				menu.Menu_Access		= 1	
			ORDER BY
				Menu_Code ASC				
		END
END

