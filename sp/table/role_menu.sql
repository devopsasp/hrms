--DROP TABLE tbl_Menu

CREATE TABLE 
			tbl_Menu
			(
				PK_Menu_Id INT not null PRIMARY KEY	,
				Menu_Name VARCHAR(40)								,
				Menu_Code VARCHAR(30)								,
				Menu_Link VARCHAR(50)								,
				Menu_Access int										,
				Parent_Id INT										,
				Select_Field int									,
				View_Status int										,
				Save_Status int										,
				Delete_Status int
			);

-- MENU Setup--
INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (1,' Setup','A','#',1,null,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (2,' Time Card','AA','/Hrms_Master/time_card/timecardsetup.aspx',1,1,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (3,' Shift Balance','AB','/Hrms_Master/time_card/shiftbalanceentry.aspx',1,1,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (4,' Over Time Slab','AC','/Hrms_Master/time_card/otslab.aspx',1,1,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (5,' Attendance Bonus','AD','/Hrms_Master/Employee/Shiftpattern.aspx',1,1,1,1,1,1)

--Menu  Settings--
INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (6,' Settings','B','#',1,null,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (-6,' Payroll','B','#',1,6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (8,' Change Password','BA','/Hrms_Company/Settings.aspx',1,6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (9,' Salary Breakups','BC','/Hrms_Master/PayRoll/Computation.aspx',1,-6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (10,' PF','BD','/Hrms_Master/PayRoll/pf.aspx',1,-6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (11,' EDLI','BE','/Hrms_Master/PayRoll/EDLI.aspx',1,-6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (12,' VPF','BF','/Hrms_PayRoll/Vpf.aspx',1,-6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (13,' ESI','BG','/Hrms_Master/PayRoll/ESI.aspx',1,-6,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (14,' PT','BH','/Hrms_PayRoll/pt.aspx',1,-6,1,1,1,1)


-- Menu Masters --

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (15,' Masters','C','#',1,Null,1,1,1,1)

-- Sub menu Allowances & Deductions--
INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (-15,' Allowances & Deductions','CA','#',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (16,' Allowances','CB','/Hrms_Master/PayRoll/Earnings.aspx',1,-15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (17,' Deductions','CC','/Hrms_Master/PayRoll/Deduction.aspx',1,-15,1,1,1,1)

-- Sub menu Employee--
INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (-16,' Employee','CD','#',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (18,' Employee Setup','CE','/Hrms_Company/Employee.aspx',1,-16,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (19,' Employee Position','CF','/Hrms_Master/Employee/Employee_Master.aspx',1,-16,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (20,' All Employee Basic','CG','/Hrms_Employee/AllEmpBasic.aspx',1,-16,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (21,' All Employee Allowance','CH','/Hrms_Employee/AllEmployeeAllowance.aspx',1,-16,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (22,' All Employee Deduction','CJ','/Hrms_Employee/AllEmpDeduction.aspx',1,-16,1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES(57,'User Access', 'CK','/Hrms_Employee/user_access.aspx',1,-16, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES(58,'Hierarchy', 'Cl','/Hrms_Master/Employee/hierarchy_new.aspx',1,-16, 1,1,1,1)


INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (-17,' Leave','CL','#',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (23,' Leave Setup','CM','/Hrms_Master/Leave/Leave.aspx',1,-17,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (24,' Holiday','CN','/Hrms_Master/Leave/Holiday.aspx',1,-17,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (25,' Leave Allocation','CO','/Hrms_Master/Leave/leaveAllocation.aspx',1,-17,1,1,1,1)


INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (26,' Courses','CP','/Hrms_Master/Common/Course.aspx',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (27,' Specializations','CQ','/Hrms_Master/Common/Specialization.aspx',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (28,' Skills','CQ','/Hrms_Master/Common/Skill.aspx',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (29,' Loans','CQ','/Hrms_Master/PayRoll/Loans.aspx',1,15,1,1,1,1)

INSERT INTO tbl_Menu (PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES (30,' Bank','CQ','/Hrms_Master/PayRoll/Bank.aspx',1,15,1,1,1,1)



INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES 
(31,'Time Attendance', 'D','#',1,NULL, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (-18,' Leave / On Duty', 'D','#',1,31, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (37,' Leave / On Duty Entry   ', 'DA','/Hrms_Attendance/Daily.aspx',1,-18, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (38,' Leave / On Duty Details', 'DB','/Hrms_Attendance/LeaveDetails.aspx',1,-18, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(39,' Leave Year End Process', 'DC','/Hrms_Attendance/Leaveyear.aspx',1,-18, 1,1,1,1)
	


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (32,' Download Data', 'DD','/Hrms_TimeAndAttendance/Connection.aspx',1,31, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (33,' Daily Time Card', 'DE','/Hrms_TimeAndAttendance/Dailycard.aspx',1,31, 1,1,1,1)


--INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
-- (34,' Students Time Card', 'DF','/Hrms_TimeAndAttendance/DailycardStudent.aspx',1,31,1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (35,' Manual Attendance', 'DG','/Hrms_PayRoll/payinput.aspx',1,31, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (36,' Permission', 'DH','/Hrms_Attendance/onduty.aspx',1,31, 1,1,1,1)



INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
       (40,' Payroll', 'E','#',1,NULL, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(-20,' Loan', 'E','#',1,40, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(44,' Loan Entry', 'EA','/Hrms_PayRoll/LoanEntry.aspx',1,-20, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(45,' Preclosure', 'EB','/Hrms_PayRoll/LoanPreclosure.aspx',1,-20, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(46,' Postponement', 'EC','/Hrms_PayRoll/loanpost.aspx',1,-20, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(47,' Cancellation', 'ED','/Hrms_PayRoll/Loan_cancel.aspx',1,-20, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (41,' Salary Period ', 'EE','/Hrms_PayRoll/salary_period.aspx',1,40, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (-19,' Salary Change Entry ', 'E','#',1,40, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (42,' Employee VS Allowance', 'EF','/Hrms_PayRoll/Employee_Earnings.aspx',1,-19, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
 (43,' Employee VS Deduction', 'EG','/Hrms_PayRoll/Employee_Deductions.aspx',1,-19, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(48,' Pay Slip Process  ', 'EH','/Hrms_PayRoll/PaySlip_process.aspx',1,40, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(49,' PF Nominee ', 'EI','/Hrms_PayRoll/Pf_NominiDetails.aspx',1,40, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(50,' Final Settlement', 'EJ','/Hrms_PayRoll/Final_settlement_new.aspx',1,40, 1,1,1,1)



INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(51,' Reports', 'F','#',1,NULL, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(52,' Employee General', 'FA','/PayrollReports/EmployeeGeneral.aspx',1,51, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(53,' Attendance', 'FB','/PayrollReports/Attendance.aspx',1,51, 1,1,1,1)

INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(54,' PF Monthly', 'FC','/PayrollReports/PFReport.aspx',1,51, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(55,' ESI Monthly', 'FD','/PayrollReports/EsiReport.aspx',1,51, 1,1,1,1)


INSERT INTO tbl_Menu(PK_Menu_Id,Menu_Name,Menu_Code,Menu_Link,Menu_Access,Parent_Id,Select_Field,View_Status,Save_Status,Delete_Status) VALUES
(56,' Payslip', 'FE','/PayrollReports/Payslip.aspx',1,51, 1,1,1,1)





SELECT * FROM tbl_Menu
