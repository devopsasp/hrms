USE Hesperus_Hrms
GO
/****** Object:  StoredProcedure [dbo].[spr_role_save]    Script Date: 01-08-2019 11:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE spr_role_save
(
	@i_a_role_id		INT = 0,
	@s_a_role_name		NVARCHAR(50) = 'fdfsd',
	@s_a_description	NVARCHAR(250) = 'dfsd',
	@i_a_user_id		INT = 0
)
AS
BEGIN
	   DECLARE @i_l_result	INT
		SET NOCOUNT ON	
			IF(@i_a_role_id = 0)
			BEGIN
				IF EXISTS(SELECT pk_role_id FROM tbl_role WHERE role_name = @s_a_role_name)
				BEGIN
					SET @i_l_result = 0
				END
				ELSE
				BEGIN					
					INSERT INTO tbl_role
					(
						role_name,
						Description,
						created_date,
						fk_created_user_id
					)
					VALUES
					(
						@s_a_role_name,
						@s_a_description,
						getdate(),
						@i_a_user_id
					)
					SET @i_l_result = 1
				END
			END	
			ELSE
			BEGIN
				IF EXISTS(SELECT pk_role_id FROM tbl_role WHERE role_name = @s_a_role_name AND PK_Role_Id <> @i_a_role_id)
				BEGIN
					SET @i_l_result = 0
				END
				ELSE
				BEGIN
					UPDATE
						tbl_role
					SET
						role_name = @s_a_role_name,
						Description = @s_a_description,
						modified_date = getdate(),
						fk_modified_user_id = @i_a_user_id
					WHERE
						PK_Role_Id = @i_a_role_id
					SET @i_l_result = 1
				END
			END

			SELECT @i_l_result result
				       
		SET NOCOUNT OFF
END

