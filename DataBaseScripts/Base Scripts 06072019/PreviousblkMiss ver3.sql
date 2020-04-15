Miss block script // For Logger Performance

drop table if exists PreviousBlkMiss;
drop table if exists tsPreviousBlkMiss;
DROP ROW TYPE if exists rtPreviousBlkMiss RESTRICT;

create row type etools_dev.rtPreviousBlkMiss
(
tstamp datetime year to fraction(5),
timestampID int,	
BlockNo varchar(100),
IsPending Int,
issend integer
);


drop table if exists tsPreviousBlkMiss;
execute procedure ifx_allow_newline('t');
create table etools_dev.tsPreviousBlkMiss
(METERID int8 NOT NULL,
series timeseries(rtPreviousBlkMiss),
primary key(METERID)) in dbloadsurvey;

execute procedure tscontainercreate('tsconPreviousBlkMiss','dbloadsurvey','rtPreviousBlkMiss',1024,1024,'2015-01-01 00:00:00.00000'::datetime year to fraction(5), 'day',5,8, 'dbloadsurvey', 1, 16, 16);

execute procedure ifx_allow_newline('t');
drop table if exists PreviousBlkMiss;
execute procedure TSCreateVirtualTab
('etools_dev.PreviousBlkMiss',
'etools_dev.tsPreviousBlkMiss','calendar(cal_15min),
container(tsconPreviousBlkMiss),irregular');
