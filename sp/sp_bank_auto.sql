
CREATE PROCEDURE sp_bank_auto
(
@bank int
)
AS
 SELECT * FROM paym_Bank WHERE v_BankCode LIKE +@bank+'%' 
GO

--drop PROCEDURE sp_bank_auto