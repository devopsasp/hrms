
Go
CREATE PROCEDURE spr_role_access_save
(
	@i_a_role_id		INT  = 1	,
	@s_a_role_access	VARCHAR(MAX) = '6^2^1^1^0^~13^1^1^0^0^~12^1^1^0^0^~3^1^1^0^0^~2^1^1^1^0^~5^1^1^1^0^~7^1^1^1^0^~4^1^1^1^0^~8^1^1^1^0^~' ,
	@i_a_log_user_id	varchar(30)= 'demo'
	)
AS
BEGIN
		
		DECLARE @STORE							VARCHAR(20)
		DECLARE @SPLITROW						CHAR(1)	
		DECLARE @SPLITCOL						CHAR(1)
		DECLARE @i_l_menu_id					INT
		DECLARE @s_l_view_status				VARCHAR(2)
		DECLARE @s_l_save_status				VARCHAR(2)		
		DECLARE @s_l_delete_status				VARCHAR(2)									
		DECLARE @I								INT
		DECLARE @J								INT
		DECLARE @i_l_active						INT
		DECLARE @i_l_result						INT

		SET @SPLITROW	=	'~'
		SET @SPLITCOL	=	'^'	
	
		WHILE CHARINDEX(@SPLITROW,@s_a_role_access,0) > 0
		BEGIN
			SET @I = CHARINDEX(@SPLITROW,@s_a_role_access,0)
			SET @STORE = SUBSTRING (@s_a_role_access, 0, @I)			
			WHILE CHARINDEX(@SPLITCOL,@STORE,0) > 0
			BEGIN
				SET @J = CHARINDEX(@SPLITCOL,@STORE,0)
				SET @i_l_menu_id = SUBSTRING (@STORE, 0, @J)
					
				SET @STORE = SUBSTRING(@STORE,@J+1,LEN(@STORE))					
				SET @J = CHARINDEX(@SPLITCOL,@STORE,0)
				SET @s_l_view_status = SUBSTRING (@STORE, 0, @J)
					
				SET @STORE = SUBSTRING(@STORE,@J+1,LEN(@STORE))					
				SET @J = CHARINDEX(@SPLITCOL,@STORE,0)
				SET @s_l_save_status = SUBSTRING (@STORE, 0, @J)
					
				SET @STORE = SUBSTRING(@STORE,@J+1,LEN(@STORE))
				SET @J = CHARINDEX(@SPLITCOL,@STORE,0)
				SET @s_l_delete_status = SUBSTRING (@STORE, 0, @J)
					

				IF(@s_l_view_status = 1 OR @s_l_save_status = 1 OR @s_l_delete_status = 1)
				BEGIN
					SET @i_l_active = 1
				END
				ELSE
				BEGIN
					SET @i_l_active = 0						
				END
				
				BEGIN TRY
					IF NOT EXISTS(SELECT FK_Role_Id FROM tbl_Role_Access where FK_Role_Id = @i_a_role_id AND Fk_Menu_Id = @i_l_menu_id)
					BEGIN
						INSERT INTO tbl_Role_Access
						(     
							FK_Role_Id			,
							FK_Menu_Id			,
							View_Status			,
							Save_Status			,
							Delete_Status		,
							
							Active				,
							Created_Date		,
							FK_Created_User_Id
						) 
						VALUES
						(
							@i_a_role_id		,  
							@i_l_menu_id		,  
							@s_l_view_status	,
							@s_l_save_status	,     
							@s_l_delete_status	,
							
							@i_l_active			,
							GETDATE()			,
							(SELECT pn_EmployeeID FROM paym_Employee where EmployeeCode=@i_a_log_user_id)
						)
						SET @i_l_result = 1
					END
					ELSE
					BEGIN
						UPDATE
							tbl_Role_Access
						SET
							View_Status			=	@s_l_view_status	,
							Save_Status			=	@s_l_save_status	,
							Delete_Status		=	@s_l_delete_status	,						
							Active				=	@i_l_active			,
							Modified_Date		=	GETDATE()			,
							FK_Modified_User_Id	=	(SELECT pn_EmployeeID FROM paym_Employee where EmployeeCode=@i_a_log_user_id)
						WHERE
							FK_Role_Id			=	@i_a_role_id		AND
							FK_Menu_Id			=	@i_l_menu_id									
						SET @i_l_result = 1
					END
				END TRY
				BEGIN CATCH
					SET @i_l_result = -1				
					RETURN
				END CATCH
				SET @STORE = SUBSTRING(@STORE,@J+1,LEN(@STORE))
			END
			SET @s_a_role_access = SUBSTRING(@s_a_role_access,@I+1,LEN(@s_a_role_access))
		END

		SELECT @i_l_result result

END