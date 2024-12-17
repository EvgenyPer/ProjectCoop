package com.example.prj.controller;

import com.example.prj.entity.Role;
import com.example.prj.service.RoleService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Аннотация @RestController указывает, что этот класс является контроллером REST API,
// который возвращает данные в формате JSON.
@RestController
// Аннотация @RequestMapping задаёт базовый URL для всех маршрутов контроллера.
@RequestMapping("/api/roles")
public class RoleController {

    // Финальное поле roleService используется для вызова методов сервиса RoleService.
    private final RoleService roleService;

    // Конструктор для внедрения зависимости RoleService.
    public RoleController(RoleService roleService) {
        this.roleService = roleService;
    }

    // Обработчик GET-запросов на URL /api/roles.
    // Возвращает список всех ролей.
    @GetMapping
    public List<Role> getAllRoles() {
        return roleService.getAllRoles();
    }

    // Обработчик POST-запросов на URL /api/roles.
    // Добавляет новую роль в базу данных.
    @PostMapping
    public Role addRole(@RequestBody Role role) {
        return roleService.saveRole(role);
    }

    // Обработчик PUT-запросов на URL /api/roles/{id}.
    // Обновляет роль по её идентификатору.
    @PutMapping("/{id}")
    public Role updateRole(@PathVariable Long id, @RequestBody Role role) {
        return roleService.updateRole(id, role);
    }

    // Обработчик DELETE-запросов на URL /api/roles/{id}.
    // Удаляет роль по её идентификатору.
    @DeleteMapping("/{id}")
    public void deleteAuthor(@PathVariable Long id) {
        roleService.deleteById(id);
    }
}

