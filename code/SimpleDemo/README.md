# Project setup

## Prerequisites

### RabbitMq (optional)

- Install RabbitMq, activate [management plugin](https://www.rabbitmq.com/management.html)
- Create virtual host `simple` within RabbitMq using the web interface and add default user to the newly created virtual host.
- default username: `guest`, default password: `guest`

### Database (Eventstore)

- Right-click `Simple.Web.App_Data` folder > `Add new item` > `SQL Server Database` > Name: `SimpleEventStore.mdf`.
- Execute SQL script `create-eventstore.sql` (location: `code\SimpleDemo\sql-scripts\`)

## Configuration

`MassTransit` can be used with an in-memory bus (instead of RabbitMq). See [here](http://docs.masstransit-project.com/en/latest/configuration/transports/in_memory.html) for details.
To use the in-memory bus, ensure the property `UseInMemoryBus` in `Web.config` is set to `true`.
In case the property is set to `false` RabbitMq must be installed and running.
