<?php
$func_name = $_GET['funcName'];

if ($func_name == "pgauge_data")
	{
		$live_value = 60;
		echo "value=$live_value";
	}
	
if ($func_name == "dgauge_data")
	{
		// 0 to 60 as in one minute.
		$live_mvalue = 56;
		$live_hvalue = 28;
		echo "value=$live_mvalue&hist=$live_hvalue";
	}

if ($func_name == "dpgauge_data")
	{
		// 0 to 100 %.
		$live_mvalue = 80;
		$live_hvalue = 30;
		echo "value=$live_mvalue&hist=$live_hvalue";
	}
	
if ($func_name == "bar_data")
	{
		// 0 to 60 as in one minute.
		$live_value = 70;
		$live_range = 90;
		echo "value=$live_value&range=$live_range";
	}
	
if ($func_name == "res_data")
	{
		// 0 to 100 as in one minute.
		$live_value = 1;
		echo "value=$live_value";
	}
	
if ($func_name == "meter_data")
	{
		// 0 to 100 as in one minute.
		$live_value = 45;
		$live_range = 100;
		echo "value=$live_value&range=$live_range";
	}

if ($func_name == "pmeter_data")
	{
		// 0 to +100 or -100.
		$live_value = -45;
		$live_range = 100;
		echo "value=$live_value&range=$live_range";
	}
	
if ($func_name == "status_data")
	{
		// 0 or 1 
		$live_value = 1;
		echo "value=$live_value";
	}
	
if ($func_name == "temp_data")
	{
		$live_value = 75;
		$live_range = 100;
		echo "value=$live_value&range=$live_range";
	}	
	
if ($func_name == "iogauge_data")
	{
		$in_range   = 500;
		$out_range  = 500;
		$in_value   = 200;
		$out_value  = 300;
		$avg_in_value  = 160;
		$avg_out_value = 230;
		echo "in_range=$in_range&out_range=$out_range&in_value=$in_value&out_value=$out_value&avg_in_value=$avg_in_value&avg_out_value=$avg_out_value";
	}
	
if ($func_name == "digital_data")
	{
		$value_a  = 200;
		$value_b  = 300;
		echo "value_a=$value_a&value_b=$value_b";
	}
?>
