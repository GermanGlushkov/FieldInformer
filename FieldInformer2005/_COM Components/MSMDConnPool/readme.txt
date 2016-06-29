Connection Pooling Sample Code

1. This is sample code that demonstrates how to implement connection pooling for Microsoft SQL Server Olap Services. See comments in MSMDConnPool.cls to find out how is the pool implemented.  Sample.asp and global.asa are active server files that demonstrate how to use the connection pool.

2. Files included:
	global.asa		- Active Server Application file (creates the connection pool)
	sample.asp		- Active server page that gets connections from the connection pool 
	msmdconnpool.dll	- a dll that implements the connection pooling

	MSMDConnPool Sample Code
		MSMDConnPool.cls	
		MSMDConnPool.vbp

3. Instructions:
	copy global.asa, sample.asp and msmdconnpool.dll into your IIS virtual directory
	register msmdconnpool.dll 
	edit sample.asp 
		- change the server and database name in the connection string
		- change the cube name and the query 
		  (if necessary, the default query works with the Foodmart sample database)

	