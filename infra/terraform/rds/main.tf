resource "aws_db_instance" "example" {
  identifier           = "example-db"
  engine               = "postgres"
  instance_class       = var.db_instance_class
  name                 = var.db_name
  username             = var.db_username
  password             = var.db_password
  publicly_accessible = false
}

resource "local_file" "script" {
  filename = "${path.module}/create_tables.sql"
  content  = file("${path.module}/scripts/create_tables.sql")
}

resource "null_resource" "execute_script" {
  triggers = {
    instance_identifier = aws_db_instance.example.id
  }

  provisioner "local-exec" {
    command = <<-EOT
      psql -h ${aws_db_instance.example.address} -U ${var.db_username} -d ${var.db_name} -f ${local_file.script.filename}
    EOT
  }
}

output "db_instance_endpoint" {
  value = aws_db_instance.example.endpoint
}