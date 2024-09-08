# Use the official Microsoft SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

# Expose SQL Server port
EXPOSE 1433

# Default command to start SQL Server
CMD ["/opt/mssql/bin/sqlservr"]
