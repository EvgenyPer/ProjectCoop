package com.example.prj.repository;

import com.example.prj.entity.Role;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

// Аннотация @Repository указывает, что это интерфейс репозитория Spring Data JPA,
// который предоставляет методы для работы с базой данных.
@Repository
// Интерфейс JpaRepository предоставляет стандартные CRUD-методы для сущности Role.
// Аргументы <Role, Long> указывают сущность Role и тип идентификатора (Long).
public interface RoleRepository extends JpaRepository<Role, Long> {
}
