#include "data/transaction.h"

#include <utility>

// transaction class implementation
std::uint64_t transaction::id() const
{
    return id_;
}
void transaction::id(std::uint64_t transaction_id)
{
    id_ = transaction_id;
}

const std::string& transaction::account_code() const
{
    return account_code_;
}
void transaction::account_code(std::string code)
{
    account_code_ = std::move(code);
}

const std::string& transaction::label() const
{
    return label_;
}
void transaction::label(std::string transaction_label)
{
    label_ = std::move(transaction_label);
}

const std::optional<std::string>& transaction::note() const
{
    return note_;
}
void transaction::note(std::optional<std::string> transaction_note)
{
    note_ = std::move(transaction_note);
}

const std::optional<std::string>& transaction::payment_method() const
{
    return payment_method_;
}
void transaction::payment_method(std::optional<std::string> method)
{
    payment_method_ = std::move(method);
}

const std::optional<std::string>& transaction::check_number() const
{
    return check_number_;
}
void transaction::check_number(std::optional<std::string> ck_number)
{
    check_number_ = std::move(ck_number);
}

std::chrono::system_clock::time_point transaction::created_at() const
{
    return created_at_;
}
void transaction::created_at(std::chrono::system_clock::time_point creation_time)
{
    created_at_ = creation_time;
}

std::chrono::year_month_day transaction::accounting_date() const
{
    return accounting_date_;
}
void transaction::accounting_date(std::chrono::year_month_day date)
{
    accounting_date_ = date;
}

std::optional<std::chrono::year_month_day> transaction::value_date() const
{
    return value_date_;
}
void transaction::value_date(std::optional<std::chrono::year_month_day> date)
{
    value_date_ = date;
}

bool transaction::is_reconciled() const
{
    return is_reconciled_;
}
void transaction::is_reconciled(bool reconciled)
{
    is_reconciled_ = reconciled;
}

std::int64_t transaction::amount() const
{
    return amount_;
}
void transaction::amount(std::int64_t cents)
{
    amount_ = cents;
}