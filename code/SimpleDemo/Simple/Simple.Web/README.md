# Project setup

## Prerequisites

### RabbitMq

- Install RabbitMq
- Create virtual host `simple` within RabbitMq using the web interface and add default user to the newly created virtual host.
- default username: `guest`, default password: `guest`

### Database (Eventstore)

- Right-click `Simple.Web.App_Data` folder > `Add new item` > `SQL Server Database` > Name: `SimpleEventStore.mdf`.
- Execute SQL script `create-eventstore.sql` (location: `code\SimpleDemo\sql-scripts\`)
