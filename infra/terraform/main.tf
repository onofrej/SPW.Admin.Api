provider "aws" {
  region = var.region
}

module "rds" {
  source = "./rds"
  db_name = var.db_name
  db_username = var.db_username
  db_password = var.db_password
  db_instance_class = var.db_instance_class
}

# Define variables
variable "region" {
  description = "AWS region"
  default     = "us-west-2"
}

variable "db_name" {
  description = "Database name"
  default     = "your_database_name"
}

variable "db_username" {
  description = "Database username"
  default     = "your_username"
}

variable "db_password" {
  description = "Database password"
  default     = "your_password"
}

variable "db_instance_class" {
  description = "Database instance class"
  default     = "db.t3.micro"
}